﻿using System;
using System.Linq;
using General.Auth;
using General.Repository;
using User.API.DTOs;
using User.API.Infrastructure.Exceptions;
using User.API.Infrastructure.Repositories.Users.UserAccounts;
using User.API.Model.Users.UserAccounts;

namespace User.API.Services.CredentialsService
{
    public class CredentialsService
    {
        private readonly RepositoryWrapper<IUserAccountRepository> _accountWrapper;
        private readonly JwtManager _jwtManager;

        public CredentialsService(
            IUserAccountRepository accountRepository, string jwtSecret
        )
        {
            _jwtManager = new JwtManager(jwtSecret);
            _accountWrapper = new RepositoryWrapper<IUserAccountRepository>(accountRepository);
        }

        public string Login(LoginCredentials credentials)
        {
            UserAccount userAccount = _accountWrapper.Repository.GetMatching(account 
                => 
                account.Credentials.Email == credentials.Email
                && account.Credentials.Password == credentials.Password    
            ).FirstOrDefault();

            if (userAccount != default)
            {
                if (!userAccount.IsActivated) throw new BadRequestException("Account not activated.");
                return _jwtManager.Encode(MapAccountToUserToken(userAccount));
            }

            return null;
        }

        private UserToken MapAccountToUserToken(UserAccount userAccount)
        {
            return new UserToken
            {
                Gen = DateTime.Now.Ticks,
                Exp = DateTime.Now.AddHours(1).Ticks,
                Username = userAccount.Credentials.Username,
                Role = userAccount.Role,
                UserId = userAccount.Id
            };
        }
    }
}