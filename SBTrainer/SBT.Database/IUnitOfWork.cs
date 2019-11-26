using SBT.Database.Entities;
using SBT.Database.Repository;
using System;
using System.Threading.Tasks;

namespace SBT.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Account> AccountRepository { get; }

        IGenericRepository<Bet> BetRepository { get; }

        IGenericRepository<Game> GameRepository { get; }

        IGenericRepository<Site> SiteRepository { get; }

        IGenericRepository<SportData> SportDataRepository { get; }

        IGenericRepository<Sport> SportRepository { get; }

        Task Commit();

        void Rollback();
    }
}
