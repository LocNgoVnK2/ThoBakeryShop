using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        IMapper _mapper;
        public CategoryController(ICategoryService categoryService , IMapper mapper) { 
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryPageViewModel model)
        {
            try
            {
                Category category = _mapper.Map<Category>(model.NewCategory);
                IQueryable<Category> categories = await _categoryService.GetCategories();
                bool result = categories.Any(x => x.CategoryName.Equals(category.CategoryName));
                if (result)
                {
                    category = await categories.Where(e => e.CategoryName.Equals(category.CategoryName)).FirstOrDefaultAsync();
                    category.IsUsed = true;
                    await _categoryService.UpdateCategory(category);
                   
                }
                else
                {
                    await _categoryService.InsertCategory(category);
                    
                }
                
                return RedirectToAction("Category", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
   
        public ActionResult EditCategory(int id)
        {
            return RedirectToAction("EditCategory", "DashBoard", new { id = id });
          
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryViewModel model)
        {
            try
            {
                Category category = _mapper.Map<Category>(model);
                await _categoryService.UpdateCategory(category);
                return RedirectToAction("Category", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {

                Category category = await _categoryService.GetCategory(id);
                category.IsUsed =false;
                await _categoryService.UpdateCategory(category);
                return RedirectToAction("Category", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
