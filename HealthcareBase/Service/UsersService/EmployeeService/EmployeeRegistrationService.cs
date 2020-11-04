// File:    EmployeeRegistrationService.cs
// Author:  Lana
// Created: 02 June 2020 13:32:05
// Purpose: Definition of Class EmployeeRegistrationService

using System.Linq;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.UserAccounts;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.UsersService.EmployeeService
{
    public class EmployeeRegistrationService
    {
        private readonly RepositoryWrapper<DoctorRepository> doctorRepository;
        private readonly RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository;
        private readonly RepositoryWrapper<EmployeeRepository> employeeRepository;

        public EmployeeRegistrationService(RepositoryWrapper<DoctorRepository> doctorRepository,
            RepositoryWrapper<EmployeeRepository> employeeRepository, RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository)
        {
            this.doctorRepository = doctorRepository;
            this.employeeRepository = employeeRepository;
            this.employeeAccountRepository = employeeAccountRepository;
        }

        public bool IsUsernameUnique(string username)
        {
            var accounts = employeeAccountRepository.Repository.GetAll();
            if (accounts.Any(acc => acc.Username.Equals(username)))
                return false;
            return true;
        }

        public bool IsRegistered(string jmbg)
        {
            var accounts = employeeAccountRepository.Repository.GetAll();
            if (accounts.Any(acc => acc.Employee.Jmbg.Equals(jmbg)))
                return true;
            return false;
        }

        public EmployeeAccount RegisterDoctor(Doctor doctor, string username, string password)
        {
            if (doctor.Specialties is null || doctor.Department is null || username == "" || password == "")
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new BadRequestException();

            var newEmployeeAccount = new EmployeeAccount
            {
                Employee = doctor,
                EmployeeType = EmployeeType.Doctor,
                Username = username,
                Password = password
            };

            doctorRepository.Repository.Create(doctor);
            return employeeAccountRepository.Repository.Create(newEmployeeAccount);
        }

        public EmployeeAccount RegisterSecretary(Employee secretary, string username, string password)
        {
            if (secretary is null || username == "" || password == "")
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new BadRequestException();

            var newEmployeeAccount = new EmployeeAccount
            {
                Employee = secretary,
                EmployeeType = EmployeeType.Secretary,
                Username = username,
                Password = password
            };

            employeeRepository.Repository.Create(secretary);
            return employeeAccountRepository.Repository.Create(newEmployeeAccount);
        }

        public void Fire(Employee employee)
        {
            var employeeAccount = employeeAccountRepository.Repository.GetByEmployee(employee);

            employeeAccountRepository.Repository.Delete(employeeAccount);
            employee.Status = EmployeeStatus.Former;
            if (employee.GetType().Equals(typeof(Doctor)))
                doctorRepository.Repository.Update((Doctor) employee);
            else
                employeeRepository.Repository.Update(employee);
        }
    }
}