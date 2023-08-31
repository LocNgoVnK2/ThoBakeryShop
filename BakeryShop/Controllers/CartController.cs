using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BakeryShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;

        public CartController( IProductsService productsService) {
            this._productsService = productsService;
        }
        public async Task<ActionResult> Index(int id, int quantity)
        {
            if (id != 0)
            {
                var oldItemData = HttpContext.Session.GetString("cart");
                if(oldItemData != null)
                { // thêm vào card đã có item

                    List<CartItemVewModel> oldItemList = new List<CartItemVewModel>();

                    oldItemList = JsonConvert.DeserializeObject<List<CartItemVewModel>>(oldItemData);
                    
                    Product product = await _productsService.GetProduct(id);
                    if (oldItemList.FirstOrDefault(item => item.ProductId == product.ProductID) != null)
                    {

                        oldItemList.FirstOrDefault(item => item.ProductId == product.ProductID).Quantity += quantity;

                    }
                    else
                    {
                        CartItemVewModel cartItemVewModel = new CartItemVewModel()
                        {
                            Price = (Double)product.Price,
                            Quantity = quantity,
                            ProductName = product.ProductName,
                            ProductImage = product.Image,
                            ProductId = product.ProductID
                        };
                        oldItemList.Add(cartItemVewModel); // Thêm sản phẩm mới vào danh sách
                    }
                    string serializedItemList = JsonConvert.SerializeObject(oldItemList);
                    HttpContext.Session.SetString("cart", serializedItemList);

                }
                else
                { // thêm mới cart còn trẳng
                    Product product = await _productsService.GetProduct(id);
                    List<CartItemVewModel> cartItemVewModels = new List<CartItemVewModel>();
                    CartItemVewModel cartItemVewModel = new CartItemVewModel()
                    {
                        Price = (Double)product.Price,
                        Quantity = quantity,
                        ProductName = product.ProductName,
                        ProductImage = product.Image,
                        ProductId = product.ProductID
                    };
                    cartItemVewModels.Add(cartItemVewModel);
                    var item = cartItemVewModels;

                    // Lưu danh sách cartItemVewModels vào session
                    string serializedItemList = JsonConvert.SerializeObject(item);
                    HttpContext.Session.SetString("cart", serializedItemList);
                }
              
            }
            var cartItemData = HttpContext.Session.GetString("cart");
            List<CartItemVewModel> cartItemList = new List<CartItemVewModel>();

            if (!string.IsNullOrEmpty(cartItemData))
            {
                cartItemList = JsonConvert.DeserializeObject<List<CartItemVewModel>>(cartItemData);
            }

            return View(cartItemList);
        }

    }
}
