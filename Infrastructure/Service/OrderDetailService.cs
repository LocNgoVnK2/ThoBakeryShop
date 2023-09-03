using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IOrderDetailService
    {
        Task<IQueryable<OrderDetail>> GetOrderDetails();
        Task<OrderDetail> GetOrderDetail(int id);
        Task InsertOrderDetail(OrderDetail orderDetail);
        Task UpdateOrderDetail(OrderDetail orderDetail);
        Task DeleteOrderDetail(OrderDetail orderDetail);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }

        public async Task<IQueryable<OrderDetail>> GetOrderDetails()
        {
            return await Task.FromResult(orderDetailRepository.GetAll());
        }

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            return await orderDetailRepository.GetByIdAsync(id);
        }

        public async Task InsertOrderDetail(OrderDetail orderDetail)
        {
            await orderDetailRepository.InsertAsync(orderDetail);

        }

        public async Task UpdateOrderDetail(OrderDetail orderDetail)
        {
            await orderDetailRepository.UpdateAsync(orderDetail);

        }

        public async Task DeleteOrderDetail(OrderDetail orderDetail)
        {
            await orderDetailRepository.DeleteAsync(orderDetail);

        }
    }
}
