using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface ICustomerService
    {
        Task<IQueryable<Customer>> GetCustomer();
        Task<Customer> GetCustomer(int id);
        Task InsertCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);
    }

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IQueryable<Customer>> GetCustomer()
        {
            return await Task.FromResult(customerRepository.GetAll());
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await customerRepository.GetByIdAsync(id);
        }

        public async Task InsertCustomer(Customer Customer)
        {
            await customerRepository.InsertAsync(Customer);

        }

        public async Task UpdateCustomer(Customer Customer)
        {
            await customerRepository.UpdateAsync(Customer);

        }

        public async Task DeleteCustomer(Customer Customer)
        {
            await customerRepository.DeleteAsync(Customer);

        }
    }
}
