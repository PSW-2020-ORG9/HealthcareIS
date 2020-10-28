// File:    EmployeeRegistrationService.cs
// Author:  Lana
// Created: 02 June 2020 13:32:05
// Purpose: Definition of Class EmployeeRegistrationService

using System.Linq;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.UserAccounts;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.UsersService.EmployeeService
{
    public class EmployeeRegistrationService
    {
        private readonly DoctorRepository doctorRepository;
        private readonly EmployeeAccountRepository employeeAccountRepository;
        private readonly EmployeeRepository employeeRepository;

        public EmployeeRegistrationService(DoctorRepository doctorRepository,
            EmployeeRepository employeeRepository, EmployeeAccountRepository employeeAccountRepository)
        {
            this.doctorRepository = doctorRepository;
            this.employeeRepository = employeeRepository;
            this.employeeAccountRepository = employeeAccountRepository;
        }

        public bool IsUsernameUnique(string username)
        {
            var accounts = employeeAccountRepository.GetAll();
            if (accounts.Any(acc => acc.Username.Equals(username)))
                return false;
            return true;
        }

        public bool IsRegistered(string jmbg)
        {
            var accounts = employeeAccountRepository.GetAll();
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

            doctorRepository.Create(doctor);
            return employeeAccountRepository.Create(newEmployeeAccount);
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

            employeeRepository.Create(secretary);
            return employeeAccountRepository.Create(newEmployeeAccount);
        }

        public void Fire(Employee employee)
        {
            var employeeAccount = employeeAccountRepository.GetByEmployee(employee);

            employeeAccountRepository.Delete(employeeAccount);
            employee.Status = EmployeeStatus.Former;
            if (employee.GetType().Equals(typeof(Doctor)))
                doctorRepository.Update((Doctor) employee);
            else
                employeeRepository.Update(employee);
        }
    }
}