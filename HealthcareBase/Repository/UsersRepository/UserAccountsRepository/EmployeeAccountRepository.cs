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
    public interface EmployeeAccountRepository : Repository<EmployeeAccount, int>
    {
        EmployeeAccount GetByEmployee(Employee employee);

        IEnumerable<EmployeeAccount> GetAllSecretaries();

        IEnumerable<EmployeeAccount> GetAllDirectors();

        IEnumerable<EmployeeAccount> GetAllDoctors();

        EmployeeAccount GetByUsernameAndPassword(string username, string password);
    }
}