using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
