using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.Database.Repository
{
    public class SiteRepository :
        IGenericRepository<Site>
    {
        private readonly DBContext _dbContext;

        public SiteRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Site>> GetAll()
        {
            return await _dbContext.Sites.ToListAsync();
        }

        public async Task<Site> Get(object id)
        {
            return await _dbContext.Sites.FindAsync(id);
        }

        public void Insert(Site entity)
        {
            _dbContext.Sites.Add(entity);
        }

        public void Update(Site entity)
        {
            _dbContext.Sites.Update(entity);
        }

        public async void Delete(object id)
        {
            var site = await _dbContext.Sites.FindAsync(id);
            if (site != null)
            {
                _dbContext.Sites.Remove(site);
            }
        }
    }
}
