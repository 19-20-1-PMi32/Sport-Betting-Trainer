using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    class SportDataService : ISportDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SportDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SportData>> GetAllSportData()
        {
            return await _unitOfWork.SportDataRepository.GetAll();
        }

        public async Task<SportData> GetData(string id)
        {
            return await _unitOfWork.SportDataRepository.Get(id);
        }

        public async Task<SportData> GetSportDataBySportId(string sportId)
        {
            return (await _unitOfWork.SportDataRepository.GetAll())
                .Where(x => x.SportId == sportId)
                .FirstOrDefault();
        }
    }
}
