using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class DetailController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public DetailController(IProductsService productsService, ICategoryService categoryService, IMapper mapper)
        {
            _productsService = productsService;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int id)
        {
            // Lấy thông tin chi tiết sản phẩm dựa trên ID và truyền nó tới view
            Product product = await _productsService.GetProduct(id);

            if (product != null)
            {
                // Chuyển hướng tới trang chi tiết sản phẩm và truyền model sản phẩm cho view
                ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
                return View(productViewModel);
            }
            else
            {
                return NotFound(); // Hoặc xử lý trường hợp sản phẩm không tồn tại
            }
            
        }

    }
}
