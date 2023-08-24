using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Transactions;

namespace BakeryShop.Controllers
{
    public class AccountManagementController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IEmployeeService _employeeService;
        public AccountManagementController(IAccountsService accountsService, IEmployeeService employeeService) { 
            _accountsService = accountsService;
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddUserAccount()
        {
            return RedirectToAction("AddUserAccount", "DashBoard");
        }
        [HttpPost]
        public async Task<ActionResult> AddAccount(AccountManagementViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    string employeeId;
                    Employee isEmployeeIdUnique;

                    do
                    {

                        StringBuilder result = new StringBuilder(6);
                        for (int i = 0; i < 6; i++)
                        {
                            result.Append(chars[random.Next(chars.Length)]);
                        }

                        employeeId = result.ToString();

                        isEmployeeIdUnique = await _employeeService.GetEmployee(employeeId);
                    } while (isEmployeeIdUnique != null);

                    Accounts newAccount = new Accounts()
                    {
                        Email = model.Email,
                        Username = model.Username,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                        Role = "2",
                        EmployeeID = employeeId
                    };

                    Employee newEmployee = new Employee()
                    {
                        EmployeeID = employeeId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Position = model.Position
                    };

                    await _employeeService.InsertEmployee(newEmployee);
                    await _accountsService.InsertAccount(newAccount);

                    scope.Complete(); 

                    return RedirectToAction("AccountManagement", "DashBoard");
                }
                catch (Exception ex)
                {
                  
                    scope.Dispose();
                    return BadRequest(ex.Message);
                }
            }
        }

        public async Task<ActionResult> DeleteAccount(int id)
        {
            try
            {

                Accounts existingAccounts = await _accountsService.GetAccount(id);
                await _accountsService.DeleteAccount(existingAccounts);
                return RedirectToAction("AccountManagement", "DashBoard");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateAccount(AccountManagementViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Employee employeeExist = await _employeeService.GetEmployee(model.EmployeeID);
                    Accounts accountExist = await _accountsService.GetAccount((int)model.AccountID);

                    accountExist.Email = model.Email;
                    accountExist.Username = model.Username;
                    if (model.Password != null)
                    {
                        accountExist.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    }
                    employeeExist.FirstName = model.FirstName;
                    employeeExist.LastName = model.LastName;
                    employeeExist.PhoneNumber = model.PhoneNumber;
                    employeeExist.Position = model.Position;
                    

                    await _employeeService.UpdateEmployee(employeeExist);
                    await _accountsService.UpdateAccount(accountExist);

                    scope.Complete();
                    return RedirectToAction("AccountManagement", "DashBoard");
                }
                catch (Exception ex)
                {

                    scope.Dispose();
                    return BadRequest(ex.Message);
                }
            }
            
        }

    }
}
