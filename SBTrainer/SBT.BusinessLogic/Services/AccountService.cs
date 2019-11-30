using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> GetAccount(string email)
        {
            return await _unitOfWork.AccountRepository.Get(email);
        }

        public async Task UpdateAccount(Account account)
        {
            var accountEntity = await _unitOfWork.AccountRepository.Get(account.Email);

            if (accountEntity != null)
            {
                _unitOfWork.AccountRepository.Update(account);
                await _unitOfWork.Commit();
            }
            else
            {
                throw new ArgumentException("Can't find account with such email");
            }
        }
    }
}
