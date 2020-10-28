// File:    EmployeeRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface EmployeeRepository

using Model.Users.Employee;
using Repository.Generics;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface EmployeeRepository : Repository<Employee, int>
    {
    }
}