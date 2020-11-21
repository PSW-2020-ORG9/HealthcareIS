// File:    EmployeeAccountService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class EmployeeAccountService

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public class EmployeeAccountService
    {
        private readonly RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository;

        public EmployeeAccountService(EmployeeAccountRepository employeeAccountRepository)
        {
            this.employeeAccountRepository =
                new RepositoryWrapper<EmployeeAccountRepository>(employeeAccountRepository);
        }

        public EmployeeAccount ChangePassword(EmployeeAccount account, string newPassword)
        {
            var acc = employeeAccountRepository.Repository.GetByID(account.GetKey());
            if (acc.Password == "")
                throw new BadRequestException();
            account.Password = newPassword;
            return employeeAccountRepository.Repository.Update(account);
        }

        public IEnumerable<EmployeeAccount> GetAllSecretaries()
        {
            return employeeAccountRepository.Repository.GetAllSecretaries();
        }

        public IEnumerable<EmployeeAccount> GetAllDirectors()
        {
            return employeeAccountRepository.Repository.GetAllDirectors();
        }

        public IEnumerable<EmployeeAccount> GetAllDoctors()
        {
            return employeeAccountRepository.Repository.GetAllDoctors();
        }
    }
}