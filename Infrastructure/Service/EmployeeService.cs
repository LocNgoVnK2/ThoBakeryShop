using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IEmployeeService
    {
        Task<IQueryable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task InsertEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        Task<Employee> GetEmployeeByAccountId(int id);
    }

    public class EmployeesService : IEmployeeService
    {
        private IEmployeeRepository employeeRepository;
        private IAccountsRepository accountsRepository;
        public EmployeesService(IEmployeeRepository employeeRepository, IAccountsRepository accountsRepository)
        {
            this.employeeRepository = employeeRepository;
            this.accountsRepository = accountsRepository;
        }
        public async Task<IQueryable<Employee>> GetEmployees()
        {
            return await Task.FromResult(employeeRepository.GetAll());
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await employeeRepository.GetByIdAsync(id);
        }
        public async Task<Employee> GetEmployeeByAccountId(int id)
        {
            var account = await accountsRepository.GetByIdAsync(id);
            if (account != null) {
                return await employeeRepository.GetByIdAsync(account.EmployeeID);
            }
            return null;
        }
        public async Task InsertEmployee(Employee employee)
        {
            await employeeRepository.InsertAsync(employee);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployee(Employee employee)
        {
            await employeeRepository.DeleteAsync(employee);
        }
    }
 
}
