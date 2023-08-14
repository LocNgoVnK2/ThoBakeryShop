using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using X.PagedList;


namespace BakeryShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IProductsService _productsService;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, ICategoryService categoryService, IProductsService productsService)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
            _productsService = productsService;
        }
   
        public async Task<IActionResult> Index(int? page, string searchString)
        {
            try
            {
                int pageSize = 6;
                int pageNumber = (page ?? 1);


                IQueryable<Product> listProduct = await _productsService.GetProducts();
                if (!String.IsNullOrEmpty(searchString))
                {
                    listProduct = listProduct.Where(e => e.IsUsed == true && e.ProductName.Contains(searchString)).Select(e => e);
                }
                else
                {
                    listProduct = listProduct.Where(e => e.IsUsed == true).Select(e => e);
                }
                List<ProductViewModel> products = _mapper.Map<List<ProductViewModel>>(listProduct.ToList());
                IQueryable<Category> categories = await _categoryService.GetCategories();
                foreach (ProductViewModel product in products)
                {
                    product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                }
                IPagedList<ProductViewModel> pagedProducts = await products.ToPagedListAsync(pageNumber, pageSize);
                
                return View("Index", pagedProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}