using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _unitOfWork.GameRepository.GetAll();
        }

        public async Task<Game> GetGame(int id)
        {
            return await _unitOfWork.GameRepository.Get(id);
        }

        public async Task<Game> GetGamesBySportDataId(string sportDataId)
        {
            return (await _unitOfWork.GameRepository.GetAll())
                .Where(x => x.SportDataId == sportDataId)
                .FirstOrDefault();
        }
    }
}
