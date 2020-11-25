// File:    EmployeeAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface EmployeeAccountRepository

using System.Collections.Generic;
using Model.Users.Employee;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Repository.UsersRepository.UserAccountsRepository
{
    public interface AdministrationAccountRepository : IWrappableRepository<AdministrationAccount, int>
    {
        AdministrationAccount GetByEmployee(Employee employee);

        IEnumerable<AdministrationAccount> GetAllSecretaries();

        IEnumerable<AdministrationAccount> GetAllDirectors();

        AdministrationAccount GetByUsernameAndPassword(string username, string password);
    }
}