// File:    EmployeeAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface EmployeeAccountRepository

using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public interface EmployeeAccountRepository : IWrappableRepository<EmployeeAccount, int>
    {
        EmployeeAccount GetByEmployee(Employee employee);

        IEnumerable<EmployeeAccount> GetAllSecretaries();

        IEnumerable<EmployeeAccount> GetAllDirectors();

        IEnumerable<EmployeeAccount> GetAllDoctors();

        EmployeeAccount GetByUsernameAndPassword(string username, string password);
    }
}