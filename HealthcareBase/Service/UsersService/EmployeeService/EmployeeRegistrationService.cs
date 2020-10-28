// File:    EmployeeRegistrationService.cs
// Author:  Lana
// Created: 02 June 2020 13:32:05
// Purpose: Definition of Class EmployeeRegistrationService

using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.UserAccounts;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.UsersService.EmployeeService
{
    public class EmployeeRegistrationService
    {
        private DoctorRepository doctorRepository;
        private EmployeeRepository employeeRepository;
        private EmployeeAccountRepository employeeAccountRepository;

        public EmployeeRegistrationService(DoctorRepository doctorRepository, 
            EmployeeRepository employeeRepository, EmployeeAccountRepository employeeAccountRepository)
        {
            this.doctorRepository = doctorRepository;
            this.employeeRepository = employeeRepository;
            this.employeeAccountRepository = employeeAccountRepository;
        }

        public Boolean IsUsernameUnique(String username)
        {
            IEnumerable<EmployeeAccount> accounts = employeeAccountRepository.GetAll();
            if (accounts.Any(acc => acc.Username.Equals(username)))
                return false;
            return true;
        }

        public Boolean IsRegistered(String jmbg)
        {
            IEnumerable<EmployeeAccount> accounts = employeeAccountRepository.GetAll();
            if (accounts.Any(acc => acc.Employee.Jmbg.Equals(jmbg)))
                return true;
            return false;
        }

        public Model.Users.UserAccounts.EmployeeAccount RegisterDoctor(Doctor doctor, String username, String password)
        {
            
            if (doctor.Specialties is null || doctor.Department is null || username == "" || password == "")
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new BadRequestException();

            EmployeeAccount newEmployeeAccount = new EmployeeAccount()
            {
                Employee = doctor,
                EmployeeType = EmployeeType.Doctor,
                Username = username,
                Password = password,
            };

            doctorRepository.Create(doctor);
            return employeeAccountRepository.Create(newEmployeeAccount);
        }

        public EmployeeAccount RegisterSecretary(Employee secretary, String username, String password)
        {
            if (secretary is null || username == "" || password == "")
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new BadRequestException();

            EmployeeAccount newEmployeeAccount = new EmployeeAccount()
            {
                Employee = secretary,
                EmployeeType = EmployeeType.Secretary,
                Username = username,
                Password = password,
            };

            employeeRepository.Create(secretary);
            return employeeAccountRepository.Create(newEmployeeAccount);
        }

        public void Fire(Employee employee)
        {
            EmployeeAccount employeeAccount = employeeAccountRepository.GetByEmployee(employee);

            employeeAccountRepository.Delete(employeeAccount);
            employee.Status = EmployeeStatus.Former;
            if (employee.GetType().Equals(typeof(Doctor)))
            {
                doctorRepository.Update((Doctor)employee);
            }
            else
            {
                employeeRepository.Update(employee);
            }
        }
    }
}