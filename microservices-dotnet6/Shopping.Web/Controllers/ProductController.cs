using Microsoft.AspNetCore.Authentication;
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
            var access_token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindAll(access_token);
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
                var access_token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.Create(product, access_token);

                if (response != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindById(id, access_token);

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
                var access_token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.Update(product, access_token);

                if (response != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindById(id, access_token);

            if (product != null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(ProductModel product)
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteById(product.Id, access_token);

            if (response)
                return RedirectToAction(nameof(Index));

            return View(product);
        }
    }
}
