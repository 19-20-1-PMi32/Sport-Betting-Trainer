using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.Database.Repository
{
    public class GameRepository :
        IGenericRepository<Game>
    {
        private readonly DBContext _dbContext;

        public GameRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _dbContext.Games.ToListAsync();
        }

        public async Task<Game> Get(object id)
        {
            return await _dbContext.Games.FindAsync(id);
        }

        public void Insert(Game entity)
        {
            _dbContext.Games.Add(entity);
        }

        public void Update(Game entity)
        {
            _dbContext.Games.Update(entity);
        }

        public async void Delete(object id)
        {
            var game = await _dbContext.Games.FindAsync(id);
            if (game != null)
            {
                _dbContext.Games.Remove(game);
            }
        }
    }
}
