// File:    EmployeeAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface EmployeeAccountRepository

using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public interface IAdministrationAccountRepository : IWrappableRepository<AdministrationAccount, int>
    {
        AdministrationAccount GetByEmployee(Employee employee);

        IEnumerable<AdministrationAccount> GetAllSecretaries();

        IEnumerable<AdministrationAccount> GetAllDirectors();

        AdministrationAccount GetByUsernameAndPassword(string username, string password);
    }
}