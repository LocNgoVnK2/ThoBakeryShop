using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;

        public CartController( IProductsService productsService) {
            this._productsService = productsService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
