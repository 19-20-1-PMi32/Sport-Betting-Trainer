using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.Database.Repository
{
    interface IGenericRepository<TEntity> 
        where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(object id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(object id);
    }
}
