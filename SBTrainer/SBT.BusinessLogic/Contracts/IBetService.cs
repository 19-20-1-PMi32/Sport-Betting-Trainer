using SBT.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface IBetService
    {
        Task CreateBet(Bet bet);

        Task<IEnumerable<Bet>> GetBetsForUser(string email);
    }
}
