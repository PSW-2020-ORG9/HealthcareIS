// File:    EmployeeService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class EmployeeService

using Model.Users.Employee;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Service.UsersService.EmployeeService
{
    public class EmployeeService
    {
        private readonly RepositoryWrapper<EmployeeRepository> employeeRepository;

        public EmployeeService(RepositoryWrapper<EmployeeRepository> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
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