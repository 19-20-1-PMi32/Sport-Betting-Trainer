using SBT.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface ISiteService
    {
        Task<Site> GetSite(int id);

        Task<IEnumerable<Site>> GetAllSites();

        Task<IEnumerable<Site>> GetSitesByGameId(int gameId);
    }
}
