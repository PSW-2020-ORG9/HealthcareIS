// File:    EmployeeAccountFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class EmployeeAccountFileRepository

using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.UserAccounts;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.UsersRepository.UserAccountsRepository
{
    public class EmployeeAccountFileRepository : GenericFileRepository<EmployeeAccount, int>, EmployeeAccountRepository
    {
        private DoctorRepository doctorRepository;
        private EmployeeRepository employeeRepository;
        private IntegerKeyGenerator keyGenerator;


        public EmployeeAccountFileRepository(DoctorRepository doctorRepository, EmployeeRepository employeeRepository,
            String filePath) : base(filePath)
        {
            this.doctorRepository = doctorRepository;
            this.employeeRepository = employeeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public EmployeeAccount GetByEmployee(Employee employee)
        {
            IEnumerable<EmployeeAccount> found = GetMatching(account => account.Employee.Equals(employee));
            if (found.Count() == 0)
                throw new BadRequestException();
            return found.ToList()[0];
        }
        public EmployeeAccount GetByUsernameAndPassword(String username, String password)
        {
            foreach (EmployeeAccount currentEmployeeAccount in GetAll())
            {
                if (currentEmployeeAccount.Username.Equals(username) && currentEmployeeAccount.Password.Equals(password))
                    return currentEmployeeAccount;
            }

            throw new BadReferenceException();
        }

        public IEnumerable<EmployeeAccount> GetAllSecretaries()
        {
            IEnumerable<EmployeeAccount> employeeAccounts = GetAll();
            return employeeAccounts.Where(emp => emp.EmployeeType.Equals(EmployeeType.Secretary));
        }

        public IEnumerable<EmployeeAccount> GetAllDirectors()
        {
            IEnumerable<EmployeeAccount> employeeAccounts = GetAll();
            return employeeAccounts.Where(emp => emp.EmployeeType.Equals(EmployeeType.Director));
        }

        public IEnumerable<EmployeeAccount> GetAllDoctors()
        {
            IEnumerable<EmployeeAccount> employeeAccounts = GetAll();
            return employeeAccounts.Where(emp => emp.EmployeeType.Equals(EmployeeType.Doctor));
        }
        protected override EmployeeAccount ParseEntity(EmployeeAccount entity)
        {
            try
            {
                if (entity.Employee != null)
                {
                    if (entity.EmployeeType.Equals(EmployeeType.Doctor))
                    {
                        entity.Employee = doctorRepository.GetByID(entity.Employee.GetKey());
                    }
                    else
                    {
                        entity.Employee = employeeRepository.GetByID(entity.Employee.GetKey());
                    }

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