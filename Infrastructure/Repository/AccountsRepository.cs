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
    public interface IAccountsRepository : IRepository<Accounts>
    {
    }

    public class AccountsRepository : Repository<Accounts>, IAccountsRepository
    {
        public AccountsRepository(EXDbContext context) : base(context)
        {
        }
    }
}
