using DeskMarket.Contracts;
using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static DeskMarket.Common.Constants.ValidationConstants;
using static DeskMarket.Common.Messages.ErrorMessages;

namespace DeskMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductFormModel model, string? userId)
        {
            if (model == null || userId == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!DateTime.TryParseExact(
                model.AddedOn,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime addedOn))
            {
                throw new InvalidOperationException(InvalidDateFormatErrorMessage);
            }

            Product product = new Product()
            {
                CategoryId = model.CategoryId,
                AddedOn = addedOn,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                ProductName = model.ProductName,
                SellerId = userId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddToCartAsync(string? userId, int productId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            await _context.ProductsClients
                .AddAsync(new ProductClient()
                {
                    ProductId = productId,
                    ClientId = userId
                });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductIndexModel>> AllAsync(string? userId)
        {
            return await _context.Products
                .Select(p => new ProductIndexModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    ProductName = p.ProductName,
                    HasBought = p.ProductClients.Any(pc => pc.ClientId == userId),
                    IsSeller = p.SellerId == userId
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCartModel>> AllToCartAsync(string? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Products
                .Where(p => p.ProductClients.Any(pc => pc.ClientId == userId))
                .Select(p => new ProductCartModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var models = await _context.ProductsClients
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();

            if (models.Any())
            {
                _context.ProductsClients
                    .RemoveRange(models);
            }

            Product? modelToRemove = await _context.Products
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (modelToRemove == null)
            {
                throw new ArgumentNullException(nameof(modelToRemove));
            }

            _context.Products
                .Remove(modelToRemove);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ProductFormModel model)
        {
            Product? product = await _context.Products
                .Where(p => p.Id == model.Id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ArgumentNullException(nameof(model.Id));
            }

            if (!DateTime.TryParseExact(
                model.AddedOn,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime addedOn))
            {
                throw new InvalidOperationException(InvalidDateFormatErrorMessage);
            }

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.SellerId = model.SellerId;
            product.AddedOn = addedOn;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductDeleteModel> GetDeleteModelAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductDeleteModel()
                {
                    Id= p.Id,
                    ProductName = p.ProductName,
                    Seller = p.Seller.UserName ?? string.Empty,
                    SellerId = p.SellerId
                })
                .FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(productId));
        }

        public async Task<ProductDetailsModel> GetDetailsAsync(string? userId, int productId)
        {
            if (userId == null)
            {
                userId = string.Empty;
            }

            var entity = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.ProductClients)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new ProductDetailsModel()
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
                CategoryName = entity.Category.Name,
                AddedOn = entity.AddedOn.ToString(DateFormat),
                Seller = entity.Seller.UserName ?? throw new ArgumentNullException(nameof(entity.Seller.UserName)),
                HasBought = entity.ProductClients.Any(pc => pc.ClientId == userId),
            };
        }

        public async Task<ProductFormModel> GetFormByIdAsync(string? userId, int productId)
        {
            Product? entity = await _context.Products
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (entity.SellerId != userId)
            {
                throw new InvalidOperationException();
            }

            return new ProductFormModel()
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
                AddedOn = entity.AddedOn.ToString(DateFormat),
                CategoryId = entity.CategoryId,
                SellerId = entity.SellerId
            };
        }

        public async Task RemoveFromCartAsync(string? userId, int productId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            ProductClient? model = await _context.ProductsClients
                .Where(pc => pc.ProductId == productId && pc.ClientId == userId)
                .FirstOrDefaultAsync();

            if (model != null)
            {
                _context.ProductsClients
                    .Remove(model);

                await _context.SaveChangesAsync();
            }
        }
    }
}
