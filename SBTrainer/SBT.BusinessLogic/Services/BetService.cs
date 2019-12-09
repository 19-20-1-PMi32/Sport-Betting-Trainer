using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    public class BetService : IBetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBet(Bet bet)
        {
            _unitOfWork.BetRepository.Insert(bet);
            await _unitOfWork.Commit();
        }
    }
}
