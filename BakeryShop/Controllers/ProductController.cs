using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult AddProduct()
        {
            return RedirectToAction("AddProDuct", "DashBoard");
        }
    }
}
