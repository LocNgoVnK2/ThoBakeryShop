using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(EXDbContext context) : base(context)
        {
        }
    }
}
