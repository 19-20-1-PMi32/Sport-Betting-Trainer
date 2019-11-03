using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.Database.Repository
{
    public class BetRepository :
        IGenericRepository<Bet>
    {
        private readonly DBContext _dbContext;

        public BetRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Bet>> GetAll()
        {
            return await _dbContext.Bets.ToListAsync();
        }

        public async Task<Bet> Get(object id)
        {
            return await _dbContext.Bets.FindAsync(id);
        }

        public void Insert(Bet entity)
        {
            _dbContext.Bets.Add(entity);
        }

        public void Update(Bet entity)
        {
            _dbContext.Bets.Update(entity);
        }

        public async void Delete(object id)
        {
            var bet = await _dbContext.Bets.FindAsync(id);
            if (bet != null)
            {
                _dbContext.Bets.Remove(bet);
            }
        }
    }
}
