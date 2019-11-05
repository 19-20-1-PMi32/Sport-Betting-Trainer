using System;
using System.Threading.Tasks;

namespace SBT.Database
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();

        void Rollback();
    }
}
