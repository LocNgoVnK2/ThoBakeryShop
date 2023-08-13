using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BakeryShop.Models;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductsService productsService,ICategoryService categoryService,IMapper mapper) {
            _productsService = productsService;
            _categoryService = categoryService;
            _mapper = mapper;   
        }
        public async Task<IActionResult> Index()
        {
          
                return View();
           

        }


        public IActionResult AddProduct()
        {
            return RedirectToAction("AddProduct", "DashBoard");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {

            try
            {
                if (model.ImageData != null && model.ImageData.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ImageData.CopyToAsync(memoryStream);
                        model.Image = memoryStream.ToArray();
                    }
                }

                Product product = _mapper.Map<Product>(model);
                await _productsService.InsertProduct(product);
                return RedirectToAction("Product", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> EditProduct(ProductViewModel model) {
            try
            {
                if (model.ImageData != null && model.ImageData.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ImageData.CopyToAsync(memoryStream);
                        model.Image = memoryStream.ToArray();
                    }
                }
                else
                {

                    Product existingProduct = await _productsService.GetProduct((int)model.ProductID);
                    if (existingProduct != null)
                    {
                        
                        model.Image = existingProduct.Image;
                    }
                }

                Product product = _mapper.Map<Product>(model);

                await _productsService.UpdateProduct(product);
                return RedirectToAction("Product", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
              

                Product existingProduct = await _productsService.GetProduct(id);

                existingProduct.IsUsed = false;

                 await _productsService.UpdateProduct(existingProduct);
                return RedirectToAction("Product", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
