// File:    EmployeeRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface EmployeeRepository

using User.API.Model.Users.Employees;

namespace User.API.Infrastructure.Repositories.Users.Employees.Interfaces
{
    public interface IEmployeeRepository : IWrappableRepository<Employee, int>
    {
    }
}