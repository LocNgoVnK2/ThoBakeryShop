using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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
        Task<Customer> GetCustomerByPhoneNumber(string phoneNumber);
        Task<List<Customer>> GetCustomersByPhoneNumber(string phoneNumber);
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
        
        public async Task<List<Customer>> GetCustomersByPhoneNumber(string phoneNumber)
        {
            List<Customer> customers =  await customerRepository.GetAll().Where(x => x.PhoneNumber == phoneNumber).ToListAsync();
            return customers;
        }
        public async Task<Customer> GetCustomerByPhoneNumber(string phoneNumber)
        {
            //Customer customers = await customerRepository.GetAll().Where(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
            Customer customer = await customerRepository
                .GetAll()
                .Where(x => x.PhoneNumber == phoneNumber)  
                .OrderByDescending(x => x.CustomerId) 
                .FirstOrDefaultAsync();

            return customer;
        }
    }
}
