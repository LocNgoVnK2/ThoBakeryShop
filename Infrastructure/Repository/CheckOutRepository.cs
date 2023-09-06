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
    public interface ICheckOutRepository : IRepository<CheckOut>
    {
    }

    public class CheckOutRepository : Repository<CheckOut>, ICheckOutRepository
    {
        public CheckOutRepository(EXDbContext context) : base(context)
        {
        }
    }
}
