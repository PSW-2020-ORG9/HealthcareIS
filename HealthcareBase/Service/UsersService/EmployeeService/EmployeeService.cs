// File:    EmployeeService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class EmployeeService

using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public class EmployeeService
    {
        private readonly RepositoryWrapper<EmployeeRepository> employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = new RepositoryWrapper<EmployeeRepository>(employeeRepository);
        }

        public Employee GetByID(int id)
        {
            return employeeRepository.Repository.GetByID(id);
        }

        public Employee Update(Employee employee)
        {
            return employeeRepository.Repository.Update(employee);
        }
    }
}