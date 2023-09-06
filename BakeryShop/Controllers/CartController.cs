using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Transactions;

namespace BakeryShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly ICheckOutService _checkOutService;
        public CartController(IProductsService productsService, IOrderDetailService orderDetailService, IOrderService orderService, ICheckOutService checkOutService, ICustomerService customerService)
        {
            this._productsService = productsService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _customerService = customerService;
            _checkOutService = checkOutService;
        }
        public async Task<ActionResult> Index(int id, int quantity)
        {
            if (id != 0)
            {
                var oldItemData = HttpContext.Session.GetString("cart");
                if (oldItemData != null)
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
        public async Task<ActionResult> CheckOutBill()
        {
           
            var cartItemData = HttpContext.Session.GetString("cart");
            List<CartItemVewModel> cartItemList = new List<CartItemVewModel>();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Order order = new Order()
                    {
                        OrderDate = DateTime.Now,
                        IsDone = false
                    };
                    await _orderService.InsertOrder(order);
                    Double totalPrice = 0;
                    if (!string.IsNullOrEmpty(cartItemData))
                    {

                        cartItemList = JsonConvert.DeserializeObject<List<CartItemVewModel>>(cartItemData);
                        foreach (CartItemVewModel cartItemVew in cartItemList)
                        {
                            totalPrice += cartItemVew.TotalPrice;
                            OrderDetail orderDetail = new OrderDetail()
                            {
                                ProductID = cartItemVew.ProductId,
                                OrderID = order.OrderID,
                                Quantity = cartItemVew.Quantity,
                                Subtotal = cartItemVew.TotalPrice
                                // discount tính sau 
                            };
                            await _orderDetailService.InsertOrderDetail(orderDetail);

                        }
                        order.TotalAmount = totalPrice;
                        await _orderService.UpdateOrder(order);
                    }
                    scope.Complete();
                    CheckOutViewModel checkOut = new CheckOutViewModel()
                    {
                        IdOrder = order.OrderID,
                        TotalPrice = order.TotalAmount

                    };
                    return View(checkOut);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return NotFound();

                }
            }
        }

        public async Task<ActionResult> CompleteCheckOut(CheckOutViewModel checkOutView)
        {
            // if cusmer exist then make update this cus
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    Customer customer = new Customer()
                    {
                        Address = checkOutView.Address,
                        Email = checkOutView.Email,
                        FirstName = checkOutView.FirstName,
                        LastName = checkOutView.LastName,
                        PhoneNumber = checkOutView.PhoneNumber,

                    };
                    await _customerService.InsertCustomer(customer);
                    CheckOut checkOut = new CheckOut();
                    checkOut.IdOrder = checkOutView.IdOrder;
                    checkOut.CustomerId = customer.CustomerId;
                    checkOut.IsReceived = false;
                    checkOut.Note = checkOutView.Note;
                    await _checkOutService.InsertCheckOut(checkOut);
                    scope.Complete();
                    HttpContext.Session.Remove("cart");

                    return RedirectToAction("ReviewOrder", "Cart");
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return NotFound();

                }
            }

        }
        public async Task<ActionResult> ReviewOrder(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return View();
            }
            List<CheckOutViewModel> checkOutViewModels = new List<CheckOutViewModel>();
            Customer customer = await _customerService.GetCustomerByPhoneNumber(phoneNumber);
            if(customer!=null)
            {
                List<CheckOut> checkOuts = await _checkOutService.GetListCheckOutByCustomerId((int)customer.CustomerId);
                foreach (CheckOut checkOut in checkOuts)
                {
                    Order order = await _orderService.GetOrder((int)checkOut.IdOrder);
                    CheckOutViewModel checkOutView = new CheckOutViewModel()
                    {

                        IdOrder = checkOut.IdOrder,
                        IsReceived = checkOut.IsReceived,
                        IsAccept = order.IsDone,
                        Note = checkOut.Note,
                        TotalPrice = order.TotalAmount,
                        OrderDate = order.OrderDate,
                        PhoneNumber = phoneNumber
                    };
                    checkOutViewModels.Add(checkOutView);
                }
                return View(checkOutViewModels);
            }
          return View();
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int newQuantity)
        {
            var cartItemData = HttpContext.Session.GetString("cart");
            if (!string.IsNullOrEmpty(cartItemData))
            {
                List<CartItemVewModel> cartItemList = JsonConvert.DeserializeObject<List<CartItemVewModel>>(cartItemData);
                var cartItemToUpdate = cartItemList.FirstOrDefault(item => item.ProductId == productId);
                if (cartItemToUpdate != null)
                {
                  
                    cartItemToUpdate.Quantity = newQuantity;
                    string serializedItemList = JsonConvert.SerializeObject(cartItemList);
                    HttpContext.Session.SetString("cart", serializedItemList);
                    double newTotalPrice = cartItemList.Sum(item => item.TotalPrice);
                    var responseData = new
                    {
                        success = true,
                        totalPrice = newTotalPrice,
                        totalQuantity = cartItemList.Count
                    };

                    return Json(responseData);
                }
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public IActionResult RemoveProduct(int productId)
        {
            var cartItemData = HttpContext.Session.GetString("cart");
            if (!string.IsNullOrEmpty(cartItemData))
            {
                List<CartItemVewModel> cartItemList = JsonConvert.DeserializeObject<List<CartItemVewModel>>(cartItemData);
                var cartItemToRemove = cartItemList.FirstOrDefault(item => item.ProductId == productId);
                if (cartItemToRemove != null)
                {
                    cartItemList.Remove(cartItemToRemove); // Xóa sản phẩm khỏi danh sách giỏ hàng
                    string serializedItemList = JsonConvert.SerializeObject(cartItemList);
                    HttpContext.Session.SetString("cart", serializedItemList);
                    double newTotalPrice = cartItemList.Sum(item => item.TotalPrice);
                    var responseData = new
                    {
                        success = true,
                        totalPrice = newTotalPrice,
                        totalQuantity = cartItemList.Count
                    };

                    return Json(responseData);
                }
            }
            return Json(new { success = false });
        }
    }
}
