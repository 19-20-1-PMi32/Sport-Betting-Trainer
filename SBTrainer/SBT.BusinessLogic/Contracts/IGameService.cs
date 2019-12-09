using SBT.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface IGameService
    {
        Task<Game> GetGame(int id);

        Task<IEnumerable<Game>> GetAllGames();

        Task<Game> GetGamesBySportDataId(string sportDataId);
    }
}
