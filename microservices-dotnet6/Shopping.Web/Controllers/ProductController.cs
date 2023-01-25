using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Models;
using Shopping.Web.Services.IServices;
using Shopping.Web.Utils;

namespace Shopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.FindAll();
            return View(products);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.Create(product);

                if (response != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.FindById(id);

            if (product != null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.Update(product);

                if (response != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.FindById(id);

            if (product != null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(ProductModel product)
        {
            var response = await _productService.DeleteById(product.Id);

            if (response)
                return RedirectToAction(nameof(Index));

            return View(product);
        }
    }
}
