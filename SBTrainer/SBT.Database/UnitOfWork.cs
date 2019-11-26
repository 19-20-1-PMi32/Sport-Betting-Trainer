using System.Threading.Tasks;
using SBT.Database.Repository;
using SBT.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SBT.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dbContext;

        public IGenericRepository<Account> AccountRepository { get; }

        public IGenericRepository<Bet> BetRepository { get; }

        public IGenericRepository<Game> GameRepository { get; }

        public IGenericRepository<Site> SiteRepository { get; }

        public IGenericRepository<SportData> SportDataRepository { get; }

        public IGenericRepository<Sport> SportRepository { get; }

        public UnitOfWork(DBContext dBContext)
        {
            _dbContext = dBContext;
            AccountRepository = new AccountRepository(_dbContext);
            BetRepository = new BetRepository(_dbContext);
            GameRepository = new GameRepository(_dbContext);
            SiteRepository = new SiteRepository(_dbContext);
            SportDataRepository = new SportDataRepository(_dbContext);
            SportRepository = new SportRepository(_dbContext);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                }
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
