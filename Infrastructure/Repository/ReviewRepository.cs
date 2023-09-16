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
    public interface IReviewRepository : IRepository<Review>
    {
    }

    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(EXDbContext context) : base(context)
        {
        }
    }
}
