using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.Database.Repository
{
    public class AccountRepository :
        IGenericRepository<Account>
    {
        private readonly DBContext _dbContext;

        public AccountRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account> Get(object id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public void Insert(Account entity)
        {
            _dbContext.Accounts.Add(entity);
        }

        public void Update(Account entity)
        {
            _dbContext.Accounts.Update(entity);
        }

        public async void Delete(object id)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
            }
        }
    }
}
