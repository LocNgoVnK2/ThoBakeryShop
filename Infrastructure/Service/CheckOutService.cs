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
    public interface ICheckOutService
    {
        Task<IQueryable<CheckOut>> GetCheckOuts();
        Task<CheckOut> GetCheckOut(int id);
        Task InsertCheckOut(CheckOut checkOut);
        Task UpdateCheckOut(CheckOut checkOut);
        Task DeleteCheckOut(CheckOut checkOut);
        Task<List<CheckOut>> GetListCheckOutByCustomerId(int id);
    }

    public class CheckOutService : ICheckOutService
    {
        private ICheckOutRepository checkOutRepository;

        public CheckOutService(ICheckOutRepository checkOutRepository)
        {
            this.checkOutRepository = checkOutRepository;
        }

        public async Task<IQueryable<CheckOut>> GetCheckOuts()
        {
            return await Task.FromResult(checkOutRepository.GetAll());
        }

        public async Task<CheckOut> GetCheckOut(int id)
        {
            return await checkOutRepository.GetByIdAsync(id);
        }

        public async Task InsertCheckOut(CheckOut checkOut)
        {
            await checkOutRepository.InsertAsync(checkOut);

        }

        public async Task UpdateCheckOut(CheckOut checkOut)
        {
            await checkOutRepository.UpdateAsync(checkOut);

        }

        public async Task DeleteCheckOut(CheckOut checkOut)
        {
            await checkOutRepository.DeleteAsync(checkOut);

        }
        public async Task<List<CheckOut>> GetListCheckOutByCustomerId(int id)
        {
            List<CheckOut> checkOuts = await checkOutRepository.GetAll().Where(e=>e.CustomerId==id).ToListAsync();
            return checkOuts;
        }
    }
}
