using SBT.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface ISportService
    {
        Task<Sport> GetSport(string id);

        Task<IEnumerable<Sport>> GetSports();
    }
}
