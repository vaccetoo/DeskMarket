using DeskMarket.Models;

namespace DeskMarket.Contracts
{
    public interface IProductService
    {
        Task AddAsync(ProductFormModel model, string? userId);
        Task AddToCartAsync(string? userId, int productId);
        Task<IEnumerable<ProductIndexModel>> AllAsync(string? userId);
        Task<IEnumerable<ProductCartModel>> AllToCartAsync(string? userId);
        Task DeleteAsync(int productId);
        Task EditAsync(ProductFormModel model);
        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();
        Task<ProductDeleteModel> GetDeleteModelAsync(int productId);
        Task<ProductDetailsModel> GetDetailsAsync(string? userId, int productId);
        Task<ProductFormModel> GetFormByIdAsync(string? userId, int productId);
        Task RemoveFromCartAsync(string? userId, int productId);
    }
}
