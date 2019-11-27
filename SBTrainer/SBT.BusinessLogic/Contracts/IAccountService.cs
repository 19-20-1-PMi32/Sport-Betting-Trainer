using SBT.Database.Entities;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Contracts
{
    public interface IAccountService
    {
        Task UpdateAccount(Account account);
    }
}
