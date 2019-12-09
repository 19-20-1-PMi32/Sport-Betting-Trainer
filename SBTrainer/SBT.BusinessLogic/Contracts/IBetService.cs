using SBT.Database.Entities;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface IBetService
    {
        Task CreateBet(Bet bet);
    }
}
