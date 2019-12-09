using SBT.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface ISportDataService
    {
        Task<SportData> GetData(string id);

        Task<IEnumerable<SportData>> GetAllSportData();

        Task<SportData> GetSportDataBySportId(string sportId);
    }
}
