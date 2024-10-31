using DeskMarket.Contracts;
using DeskMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeskMarket.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductIndexModel> model = await _productService.AllAsync(User.Id());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ProductFormModel model = new ProductFormModel();

            model.Categories = await _productService.GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _productService.GetCategoriesAsync();

                return View(model);
            }

            await _productService.AddAsync(model, User.Id());

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            IEnumerable<ProductCartModel> model = await _productService.AllToCartAsync(User.Id());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            await _productService.AddToCartAsync(User.Id(), id);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await _productService.RemoveFromCartAsync(User.Id(), id);

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsModel model = await _productService.GetDetailsAsync(User.Id(), id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductFormModel model = await _productService.GetFormByIdAsync(User.Id(), id);

            model.Categories = await _productService.GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _productService.GetCategoriesAsync();

                return View(model);
            }

            await _productService.EditAsync(model);

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProductDeleteModel model = await _productService.GetDeleteModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _productService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
