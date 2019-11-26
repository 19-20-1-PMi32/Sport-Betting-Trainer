using System.Threading.Tasks;
using SBT.Database.Entities;

namespace SBT.BusinessLogic.Contracts
{
    public interface IAuthenticationService
    {
       Task CreateAccount(Account account);
    }
}
