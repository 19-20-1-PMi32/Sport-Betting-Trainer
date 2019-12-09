using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    public class SiteService : ISiteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SiteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Site>> GetAllSites()
        {
            return await _unitOfWork.SiteRepository.GetAll();
        }

        public async Task<Site> GetSite(int id)
        {
            return await _unitOfWork.SiteRepository.Get(id);
        }

        public async Task<IEnumerable<Site>> GetSitesByGameId(int gameId)
        {
            return (await _unitOfWork.SiteRepository.GetAll()).Where(x => x.GameId == gameId);
        }
    }
}
