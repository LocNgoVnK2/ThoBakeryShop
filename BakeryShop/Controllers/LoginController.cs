using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BakeryShop.Controllers
{
    public class LoginController : Controller
    {
        IAccountsService accountsService;
        private readonly IMapper mapper;
        public LoginController(IAccountsService accountsService, IMapper mapper)
        {
            this.accountsService = accountsService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountsViewModel accounts)
        {
            if (accounts != null)
            {

                Accounts acc = mapper.Map<Accounts>(accounts);

                IQueryable<Accounts> list = await accountsService.GetAccounts();

                if (list.Any(a => a.Username == acc.Username && a.Password == acc.Password))
                {
                    Accounts loginUser = await list.Where(a => a.Username == acc.Username && a.Password == acc.Password).FirstOrDefaultAsync();
                    string userJson = JsonConvert.SerializeObject(loginUser);
                    HttpContext.Session.SetString("LoggedInUser", userJson);

                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {

                    ViewBag.ErrorMessage = "Sai thông tin đăng nhập.";
                    return View();

                }

            }
            else
            {
                return NotFound();
            }
        }
     
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("LoggedInUser");
            return RedirectToAction("Index", "Home");
        }
            public async Task<IActionResult> ForgotPassword()
        {

            return View();
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AccountsViewModel accounts)
        {
            if (accounts != null)
            {

                Accounts acc = mapper.Map<Accounts>(accounts);

                IQueryable<Accounts> list = accountsService.GetAccounts();

                if (list.Any(a => a.username == acc.username && a.password == acc.password))
                {
                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {

                    ViewBag.ErrorMessage = "Sai thông tin đăng nhập.";
                    return View();

                }

            }
            else
            {
                return NotFound();
            }
        }
        */


    }
}
