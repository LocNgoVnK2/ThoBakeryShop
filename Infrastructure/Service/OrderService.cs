using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IOrderService
    {
        Task<IQueryable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task InsertOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IQueryable<Order>> GetOrders()
        {
            return await Task.FromResult(orderRepository.GetAll());
        }

        public async Task<Order> GetOrder(int id)
        {
            return await orderRepository.GetByIdAsync(id);
        }

        public async Task InsertOrder(Order order)
        {
            await orderRepository.InsertAsync(order);

        }

        public async Task UpdateOrder(Order order)
        {
            await orderRepository.UpdateAsync(order);

        }

        public async Task DeleteOrder(Order order)
        {
            await orderRepository.DeleteAsync(order);

        }
    }
}
