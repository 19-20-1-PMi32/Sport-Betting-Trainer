using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.Database.Repository
{
    public class SportDataRepository :
        IGenericRepository<SportData>
    {
        private readonly DBContext _dbContext;

        public SportDataRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SportData>> GetAll()
        {
            return await _dbContext.SportData.ToListAsync();
        }

        public async Task<SportData> Get(object id)
        {
            return await _dbContext.SportData.FindAsync(id);
        }

        public void Insert(SportData entity)
        {
            _dbContext.SportData.Add(entity);
        }

        public void Update(SportData entity)
        {
            _dbContext.SportData.Update(entity);
        }

        public async void Delete(object id)
        {
            var sportData = await _dbContext.SportData.FindAsync(id);
            if (sportData != null)
            {
                _dbContext.SportData.Remove(sportData);
            }
        }
    }
}
