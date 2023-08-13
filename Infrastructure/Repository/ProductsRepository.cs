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
    public interface IProductsRepository : IRepository<Product>
    {
    }

    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(EXDbContext context) : base(context)
        {
        }
    }

}
