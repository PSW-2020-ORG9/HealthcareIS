// File:    EmployeeRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface EmployeeRepository

using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface
{
    public interface IEmployeeRepository : IWrappableRepository<Employee, int>
    {
    }
}