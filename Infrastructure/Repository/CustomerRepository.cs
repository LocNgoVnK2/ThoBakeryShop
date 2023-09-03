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
    public interface ICustomerRepository : IRepository<Customer>
    {
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EXDbContext context) : base(context)
        {
        }
    }
}
