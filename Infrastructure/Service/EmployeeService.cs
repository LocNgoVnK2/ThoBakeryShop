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
    }

    public class EmployeesService : IEmployeeService
    {
        private IEmployeeRepository employeeRepository;

        public EmployeesService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<IQueryable<Employee>> GetEmployees()
        {
            return await Task.FromResult(employeeRepository.GetAll());
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await employeeRepository.GetByIdAsync(id);
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
