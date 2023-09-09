using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Transactions;
using X.PagedList;


namespace BakeryShop.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IProductsService _productsService;
        private readonly IAccountsService _accountsService;
        private readonly IEmployeeService _employeeService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly ICheckOutService _checkOutService;
        private readonly IOrderDetailService _orderDetailService;



        public DashBoardController(IMapper mapper,
                                    ICategoryService categoryService,
                                    IProductsService productsService,
                                    IEmployeeService employeeService,
                                    IAccountsService accountsService,
                                    ICheckOutService checkOutService,
                                    IOrderService orderService,
                                    ICustomerService customerService,
                                    IOrderDetailService orderDetailService
                                    )
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _productsService = productsService;
            _employeeService = employeeService;
            _accountsService = accountsService;
            _orderService = orderService;
            _customerService = customerService;
            _checkOutService = checkOutService;
            _orderDetailService = orderDetailService;
        }

        public async Task<IActionResult> Index()
        {

            List<CheckOutBillViewModel> checkOutViewModels = new List<CheckOutBillViewModel>();

            var orders = await _orderService.GetOrders();
            orders = orders.Where(e => e.IsDone == false).Select(e => e);

            foreach (Order order in orders)
            {


                CheckOut checkOut = await _checkOutService.GetCheckOut((int)order.OrderID);
                if (checkOut != null)
                {
                    Customer customer = await _customerService.GetCustomer((int)checkOut.CustomerId);
                    CheckOutBillViewModel checkOutView = new CheckOutBillViewModel()
                    {

                        IdOrder = checkOut.IdOrder,
                        IsReceived = checkOut.IsReceived,
                        TotalPrice = order.TotalAmount,
                        OrderDate = order.OrderDate,
                        PhoneNumber = customer.PhoneNumber,
                        Address = customer.Address,
                        FirstName = customer.FirstName + " " + customer.LastName,
                    };
                    checkOutViewModels.Add(checkOutView);

                }

            }
            return View(checkOutViewModels);

        }
        //CheckOutCompleteBill
        public async Task<IActionResult> CheckOutCompleteBill()
        {

            List<CheckOutBillViewModel> checkOutViewModels = new List<CheckOutBillViewModel>();

            var orders = await _orderService.GetOrders();
            orders = orders.Where(e => e.IsDone == true).Select(e => e);

            foreach (Order order in orders)
            {
                CheckOut checkOut = await _checkOutService.GetCheckOut((int)order.OrderID);

                if (checkOut != null && checkOut.IsReceived==false)
                {
                    Customer customer = await _customerService.GetCustomer((int)checkOut.CustomerId);
                    CheckOutBillViewModel checkOutView = new CheckOutBillViewModel()
                    {

                        IdOrder = checkOut.IdOrder,
                        IsReceived = checkOut.IsReceived,
                        TotalPrice = order.TotalAmount,
                        OrderDate = order.OrderDate,
                        PhoneNumber = customer.PhoneNumber,
                        Address = customer.Address,
                        FirstName = customer.FirstName + " " + customer.LastName,
                    };
                    checkOutViewModels.Add(checkOutView);

                }

            }
            return View(checkOutViewModels);

        }
        public async Task<IActionResult> CheckOutCompleteForAdmin()
        {

            List<CheckOutBillViewModel> checkOutViewModels = new List<CheckOutBillViewModel>();

            var orders = await _orderService.GetOrders();
            orders = orders.Where(e => e.IsDone == true).Select(e => e);

            foreach (Order order in orders)
            {
                CheckOut checkOut = await _checkOutService.GetCheckOut((int)order.OrderID);

                if (checkOut != null && checkOut.IsReceived == true)
                {
                    Customer customer = await _customerService.GetCustomer((int)checkOut.CustomerId);
                    CheckOutBillViewModel checkOutView = new CheckOutBillViewModel()
                    {

                        IdOrder = checkOut.IdOrder,
                        IsReceived = checkOut.IsReceived,
                        TotalPrice = order.TotalAmount,
                        OrderDate = order.OrderDate,
                        PhoneNumber = customer.PhoneNumber,
                        Address = customer.Address,
                        FirstName = customer.FirstName + " " + customer.LastName,
                    };
                    checkOutViewModels.Add(checkOutView);

                }

            }
            return View(checkOutViewModels);

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
            catch (Exception ex)
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
        public async Task<IActionResult> AddUserAccount()
        {


            return View("AddUserAccount");
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
        public async Task<IActionResult> AccountManagement(int? page, string searchString)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);

                IQueryable<Accounts> listAccounts = await _accountsService.GetAccounts();
                IQueryable<Employee> listEmployees = await _employeeService.GetEmployees();
                if (!String.IsNullOrEmpty(searchString))
                {
                    listAccounts = listAccounts.Where(e => e.Username.Contains(searchString)).Select(e => e);

                }

                var query =
                       from Account in listAccounts
                       join Employee in listEmployees on Account.EmployeeID equals Employee.EmployeeID

                       select new
                       {
                           Employee,
                           Account
                       };
                List<AccountManagementViewModel> viewModels = new List<AccountManagementViewModel>();
                foreach (var v in query)
                {
                    AccountManagementViewModel viewModel = new AccountManagementViewModel()
                    {
                        AccountID = v.Account.AccountID,
                        Username = v.Account.Username,
                        Password = v.Account.Password,
                        Email = v.Account.Email,
                        Role = v.Account.Role,
                        EmployeeID = v.Employee.EmployeeID,
                        FirstName = v.Employee.FirstName,
                        LastName = v.Employee.LastName,
                        Position = v.Employee.Position,
                        PhoneNumber = v.Employee.PhoneNumber

                    };
                    viewModels.Add(viewModel);
                }
                IPagedList<AccountManagementViewModel> pagedAccountManagementModels = await viewModels.ToPagedListAsync(pageNumber, pageSize);
                return View("AccountManagement", pagedAccountManagementModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //EditAccount
        public async Task<IActionResult> EditAccount(int id)
        {
            Accounts account = await _accountsService.GetAccount(id);
            Employee employee = await _employeeService.GetEmployee(account.EmployeeID);
            AccountManagementViewModel model = new AccountManagementViewModel();
            if (employee != null)
            {
                model.Email = account.Email;
                model.Username = account.Username;
                model.AccountID = account.AccountID;
                model.EmployeeID = employee.EmployeeID;
                model.PhoneNumber = employee.PhoneNumber;
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
                model.Position = employee.Position;

            }
            return View("EditAccount", model);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {


            var order = await _orderService.GetOrder(orderId);
            var orderDetails = await _orderDetailService.GetOrderDetailsByOrderId(orderId);
            List<OrderDetailViewModel> orderDetailViewModels = new List<OrderDetailViewModel>();
            foreach (var orderDetail in orderDetails)
            {
                var product = await _productsService.GetProduct((int)orderDetail.ProductID);
                OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
                {
                    DiscountMoney = orderDetail.DiscountMoney,
                    OrderID = orderDetail.OrderID,
                    ProductName = product.ProductName,
                    Quantity = orderDetail.Quantity,
                    Subtotal = orderDetail.Subtotal,
                    ProductID = product.ProductID,
                    OrderDetailID = orderDetail.OrderID
                };
                orderDetailViewModels.Add(orderDetailViewModel);
            }
            CheckOutBillViewModel checkOutView = new CheckOutBillViewModel();

            CheckOut checkOut = await _checkOutService.GetCheckOut((int)order.OrderID);
            if (checkOut != null)
            {
                Customer customer = await _customerService.GetCustomer((int)checkOut.CustomerId);

                checkOutView.IdOrder = checkOut.IdOrder;
                checkOutView.IsReceived = checkOut.IsReceived;
                checkOutView.TotalPrice = order.TotalAmount;
                checkOutView.OrderDate = order.OrderDate;
                checkOutView.PhoneNumber = customer.PhoneNumber;
                checkOutView.Address = customer.Address;
                checkOutView.FirstName = customer.FirstName + " " + customer.LastName;
                checkOutView.Email = customer.Email;
                checkOutView.IsAccept = order.IsDone;
                checkOutView.IsReceived = checkOut.IsReceived;
                checkOutView.Note = checkOut.Note;
                checkOutView.orderDetails = orderDetailViewModels;
            }
         
            return Json(checkOutView);

        }
        //GetOrderDetailComplete
        [HttpGet]
        public async Task<IActionResult> GetOrderDetailComplete(int orderId)
        {


            var order = await _orderService.GetOrder(orderId);
            var orderDetails = await _orderDetailService.GetOrderDetailsByOrderId(orderId);
            List<OrderDetailViewModel> orderDetailViewModels = new List<OrderDetailViewModel>();
            foreach (var orderDetail in orderDetails)
            {
                var product = await _productsService.GetProduct((int)orderDetail.ProductID);
                OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
                {
                    DiscountMoney = orderDetail.DiscountMoney,
                    OrderID = orderDetail.OrderID,
                    ProductName = product.ProductName,
                    Quantity = orderDetail.Quantity,
                    Subtotal = orderDetail.Subtotal,
                    ProductID = product.ProductID,
                    OrderDetailID = orderDetail.OrderID
                };
                orderDetailViewModels.Add(orderDetailViewModel);
            }
            CheckOutBillViewModel checkOutView = new CheckOutBillViewModel();

            CheckOut checkOut = await _checkOutService.GetCheckOut((int)order.OrderID);
            if (checkOut != null)
            {
                Customer customer = await _customerService.GetCustomer((int)checkOut.CustomerId);
                Employee employee = await _employeeService.GetEmployeeByAccountId((int)order.AccountId);
                checkOutView.IdOrder = checkOut.IdOrder;
                checkOutView.IsReceived = checkOut.IsReceived;
                checkOutView.TotalPrice = order.TotalAmount;
                checkOutView.OrderDate = order.OrderDate;
                checkOutView.PhoneNumber = customer.PhoneNumber;
                checkOutView.Address = customer.Address;
                checkOutView.FirstName = customer.FirstName + " " + customer.LastName;
                checkOutView.Email = customer.Email;
                checkOutView.IsAccept = order.IsDone;
                checkOutView.IsReceived = checkOut.IsReceived;
                checkOutView.Note = checkOut.Note;
                checkOutView.orderDetails = orderDetailViewModels;
                checkOutView.AccountID = order.AccountId;
                checkOutView.employeeName = employee.FirstName + " " + employee.LastName;

            }
            return Json(checkOutView);

        }



        public async Task<IActionResult> CheckOutBillByEmployess(int orderId,int accountId)
        { 
             Order order = await _orderService.GetOrder(orderId);
             order.AccountId = accountId;
             order.IsDone = true;
             await _orderService.UpdateOrder(order);
           
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CheckOutBillDoneByEmployess(int orderId)
        {
            CheckOut checkOut = await _checkOutService.GetCheckOut(orderId);
            checkOut.IsReceived = true;
            
            await _checkOutService.UpdateCheckOut(checkOut);

            return RedirectToAction("CheckOutCompleteBill");
        }
        public async Task<IActionResult> RemoveOrderByAdmin(int orderId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                string validationCode = "";
                string phoneNumber = "";
                try
                {
                    Order order = await _orderService.GetOrder(orderId);
                    CheckOut checkOut = await _checkOutService.GetCheckOut(orderId);

                    IQueryable<OrderDetail> orderDetails = await _orderDetailService.GetOrderDetailsByOrderId(orderId);

                    await _checkOutService.DeleteCheckOut(checkOut);
                    foreach (OrderDetail detail in orderDetails)
                    {
                        await _orderDetailService.DeleteOrderDetail(detail);
                    }
                    await _orderService.DeleteOrder(order);

                    scope.Complete();
                    return RedirectToAction("CheckOutCompleteBill", "DashBoard");
                    //return RedirectToAction("ReviewOrder", new { validationCode = validationCode , phoneNumber =phoneNumber});
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return NotFound();
                }
            }



        }
    }

}
