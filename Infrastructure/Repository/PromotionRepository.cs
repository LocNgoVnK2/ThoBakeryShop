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
    public interface IPromotionRepository : IRepository<Promotion>
    {
    }

    public class PromotionRepository : Repository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(EXDbContext context) : base(context)
        {
        }
    }

}
