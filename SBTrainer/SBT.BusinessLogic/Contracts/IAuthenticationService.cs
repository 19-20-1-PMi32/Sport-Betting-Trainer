using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.BusinessLogic.Contracts
{
    public interface IAuthenticationService
    {
       Task<bool> CreateAccount(Account account);
    }
}
