using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BakeryShop.Controllers
{
    public class DashBoardController : Controller
    {
      
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public DashBoardController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public  IActionResult Index()
        {
                return  View();
        }
        public IActionResult Product()
        {
            return View("Product");
        }
        public IActionResult AddProduct()
        {
            return View("AddProduct");
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
