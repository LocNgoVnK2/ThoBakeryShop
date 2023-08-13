using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;


namespace BakeryShop.Controllers
{
    public class DashBoardController : Controller
    {
      private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IProductsService _productsService;
        
        public DashBoardController(IMapper mapper, ICategoryService categoryService,IProductsService productsService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _productsService = productsService;
        }

        public  IActionResult Index()
        {
                return  View();
        }
        public async Task<IActionResult> Product(int? page, string searchString)
        {
            try
            {
                int pageSize = 5;
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
               

                return View("Product", pagedProducts);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }
        public async Task<IActionResult> AddProduct()
        {
            ProductViewModel model = new ProductViewModel();

            IQueryable<Category> categories = await _categoryService.GetCategories();
            IEnumerable<CategoryViewModel> categoriesModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            model.Categories = categoriesModel;

            return View("AddProduct", model);
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            Product product = await _productsService.GetProduct(id);
            ProductViewModel productView = _mapper.Map<ProductViewModel>(product);
            IQueryable<Category> categories = await _categoryService.GetCategories();
            IEnumerable<CategoryViewModel> categoriesModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            productView.Categories = categoriesModel;

            return View("EditProduct", productView);
        }
        public async Task<IActionResult> EditCategory(int id)
        {
            Category category = await _categoryService.GetCategory(id);
            CategoryViewModel categoryView = _mapper.Map<CategoryViewModel>(category);
            return View("EditCategory", categoryView);
        }
        public async Task<IActionResult> Category()
        {
           try
           {
               var categories = await _categoryService.GetCategories();
               var categoryViews = _mapper.Map<IEnumerable<CategoryViewModel>>(categories.AsEnumerable());

               var categoryPageViewModel = new CategoryPageViewModel
               {
                        Categories = categoryViews,
                        NewCategory = new CategoryViewModel()
               };
                    return View("Category", categoryPageViewModel);
               }
               catch (Exception ex)
               {
                    return BadRequest(ex.Message);
               }
           }
    }
}
