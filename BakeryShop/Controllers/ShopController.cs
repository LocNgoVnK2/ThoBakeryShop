using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using X.PagedList;

namespace BakeryShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IProductsService _productsService;
        public ShopController(ILogger<ShopController> logger, IMapper mapper, ICategoryService categoryService, IProductsService productsService)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
            _productsService = productsService;
        }
        public async Task<IActionResult> Index(int? page, string searchString, int category, int maxPrice,int minPrice)
        {
            try
            {
                int pageSize = 4;
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
                if (category!=0)
                {
                    listProduct = listProduct.Where(e => e.IsUsed == true && e.CategoryId==category).Select(e => e);
                }
                else
                {
                    listProduct = listProduct.Where(e => e.IsUsed == true).Select(e => e);
                }
                if (maxPrice != 0)
                {
                    listProduct = listProduct.Where(e => e.IsUsed == true && e.Price>=minPrice && e.Price<=maxPrice).Select(e => e);
                }
                else
                {
                    listProduct = listProduct.Where(e => e.IsUsed == true).Select(e => e);
                }

                List<ProductViewModel> products = _mapper.Map<List<ProductViewModel>>(listProduct.ToList());
                IQueryable<Category> categories = await _categoryService.GetCategories();
                List<CategoryViewModel> categoriesModel = _mapper.Map<List<CategoryViewModel>>(categories.ToList());

                foreach (ProductViewModel product in products)
                {
                    product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                }
                
                ShopViewModel viewModel = new ShopViewModel
                {
                    PagedProducts = await products.ToPagedListAsync(pageNumber, pageSize),
                    Categories = categoriesModel
                };

                return View("Index", viewModel);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
