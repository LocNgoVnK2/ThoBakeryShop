using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IProductsService
    {
        Task<IQueryable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task DeleteProduct(Product product)
        {
           product.IsUsed = false;
           await _productsRepository.UpdateAsync(product);
        }

        public async Task<Product> GetProduct(int id)
        {
           return await _productsRepository.GetByIdAsync(id);
        }

        public async Task<IQueryable<Product>> GetProducts()
        {
           return await Task.FromResult(_productsRepository.GetAll());
        }

        public async Task InsertProduct(Product product)
        {
            await _productsRepository.InsertAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productsRepository.UpdateAsync(product);
        }
    }
}
