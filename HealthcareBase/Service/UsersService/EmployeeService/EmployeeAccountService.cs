// File:    EmployeeAccountService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class EmployeeAccountService

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Users.UserAccounts;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.UsersService.EmployeeService
{
    public class EmployeeAccountService
    {
        private readonly EmployeeAccountRepository employeeAccountRepository;

        public EmployeeAccountService(EmployeeAccountRepository employeeAccountRepository)
        {
            this.employeeAccountRepository = employeeAccountRepository;
        }

        public EmployeeAccount ChangePassword(EmployeeAccount account, string newPassword)
        {
            var acc = employeeAccountRepository.GetByID(account.GetKey());
            if (acc.Password == "")
                throw new BadRequestException();
            account.Password = newPassword;
            return employeeAccountRepository.Update(account);
        }

        public IEnumerable<EmployeeAccount> GetAllSecretaries()
        {
            return employeeAccountRepository.GetAllSecretaries();
        }

        public IEnumerable<EmployeeAccount> GetAllDirectors()
        {
            return employeeAccountRepository.GetAllDirectors();
        }

        public IEnumerable<EmployeeAccount> GetAllDoctors()
        {
            return employeeAccountRepository.GetAllDoctors();
        }
    }
}