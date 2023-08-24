using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IAccountsService
    {
        Task<IQueryable<Accounts>> GetAccounts();
        Task<Accounts> GetAccount(int id);
        Task InsertAccount(Accounts account);
        Task UpdateAccount(Accounts account);
        Task DeleteAccount(Accounts account);
    }

    public class AccountsService : IAccountsService
    {
        private IAccountsRepository accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }

        public async Task<IQueryable<Accounts>> GetAccounts()
        {
            return await Task.FromResult(accountsRepository.GetAll());
        }

        public async Task<Accounts> GetAccount(int id)
        {
            return await accountsRepository.GetByIdAsync(id);
        }

        public async Task InsertAccount(Accounts account)
        {
            await accountsRepository.InsertAsync(account);
            
        }

        public async Task UpdateAccount(Accounts account)
        {
            await accountsRepository.UpdateAsync(account);
           
        }

        public async Task DeleteAccount(Accounts account)
        {
            await accountsRepository.DeleteAsync(account);
            
        }
    }
}
