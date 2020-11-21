// File:    EmployeeAccountFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class EmployeeAccountFileRepository

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public class EmployeeAccountFileRepository : GenericFileRepository<EmployeeAccount, int>, EmployeeAccountRepository
    {
        private readonly DoctorRepository doctorRepository;
        private readonly EmployeeRepository employeeRepository;
        private readonly IntegerKeyGenerator keyGenerator;


        public EmployeeAccountFileRepository(DoctorRepository doctorRepository, EmployeeRepository employeeRepository,
            string filePath) : base(filePath)
        {
            this.doctorRepository = doctorRepository;
            this.employeeRepository = employeeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public EmployeeAccount GetByEmployee(Employee employee)
        {
            var found = GetMatching(account => account.Employee.Equals(employee));
            if (found.Count() == 0)
                throw new BadRequestException();
            return found.ToList()[0];
        }

        public EmployeeAccount GetByUsernameAndPassword(string username, string password)
        {
            foreach (var currentEmployeeAccount in GetAll())
                if (currentEmployeeAccount.Username.Equals(username) &&
                    currentEmployeeAccount.Password.Equals(password))
                    return currentEmployeeAccount;

            throw new BadReferenceException();
        }

        public IEnumerable<EmployeeAccount> GetAllSecretaries()
        {
            var employeeAccounts = GetAll();
            return employeeAccounts.Where(emp => emp.EmployeeType.Equals(EmployeeType.Secretary));
        }

        public IEnumerable<EmployeeAccount> GetAllDirectors()
        {
            var employeeAccounts = GetAll();
            return employeeAccounts.Where(emp => emp.EmployeeType.Equals(EmployeeType.Director));
        }

        public IEnumerable<EmployeeAccount> GetAllDoctors()
        {
            var employeeAccounts = GetAll();
            return employeeAccounts.Where(emp => emp.EmployeeType.Equals(EmployeeType.Doctor));
        }

        protected override EmployeeAccount ParseEntity(EmployeeAccount entity)
        {
            try
            {
                if (entity.Employee != null)
                {
                    if (entity.EmployeeType.Equals(EmployeeType.Doctor))
                        entity.Employee = doctorRepository.GetByID(entity.Employee.GetKey());
                    else
                        entity.Employee = employeeRepository.GetByID(entity.Employee.GetKey());
                }
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(EmployeeAccount entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}