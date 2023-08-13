using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
