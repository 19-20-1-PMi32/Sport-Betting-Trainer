using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.Database.Repository
{
    public class SportRepository :
        IGenericRepository<Sport>
    {
        private readonly DBContext _dbContext;

        public SportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sport>> GetAll()
        {
            return await _dbContext.Sports.ToListAsync();
        }

        public async Task<Sport> Get(object id)
        {
            return await _dbContext.Sports.FindAsync(id);
        }

        public void Insert(Sport entity)
        {
            _dbContext.Sports.Add(entity);
        }

        public void Update(Sport entity)
        {
            _dbContext.Sports.Update(entity);
        }

        public async void Delete(object id)
        {
            var sport = await _dbContext.Sports.FindAsync(id);
            if (sport != null)
            {
                _dbContext.Sports.Remove(sport);
            }
        }
    }
}
