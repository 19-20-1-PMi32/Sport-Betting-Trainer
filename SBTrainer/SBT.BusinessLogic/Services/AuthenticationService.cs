﻿using SBT.BusinessLogic.Contracts;
using SBT.Database;
using SBT.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SBT.BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAccount(Account account)
        {
            var isExist = (await _unitOfWork.AccountRepository.GetAll())
                .Any(x => x.Email == account.Email);

            if (!isExist)
            {
                _unitOfWork.AccountRepository.Insert(account);
                await _unitOfWork.Commit();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
