using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    class SportService : ISportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Sport> GetSport(string id)
        {
            return await _unitOfWork.SportRepository.Get(id);
        }

        public async Task<IEnumerable<Sport>> GetSports()
        {
            return await _unitOfWork.SportRepository.GetAll();
        }
    }
}
