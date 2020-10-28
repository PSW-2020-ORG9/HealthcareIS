// File:    LoginService.cs
// Author:  Lana
// Created: 28 May 2020 17:02:38
// Purpose: Definition of Class LoginService

using System;
using Repository.UsersRepository.UserAccountsRepository;
using Model.Users.UserAccounts;

namespace Service.UsersService.LoginService
{
    public class LoginService
    {
        private PatientAccountRepository patientAccountRepository;
        private EmployeeAccountRepository employeeAccountRepository;

        public LoginService(PatientAccountRepository patientAccountRepository, EmployeeAccountRepository employeeAccountRepository)
        {
            this.patientAccountRepository = patientAccountRepository;
            this.employeeAccountRepository = employeeAccountRepository;
        }

        public PatientAccount LogInPatient(String username, String password)
        {
            return patientAccountRepository.GetByUsernameAndPassword(username, password);
        }

        public EmployeeAccount LogInDoctor(String username, String password)
        {
            return employeeAccountRepository.GetByUsernameAndPassword(username, password);
        }

        public EmployeeAccount LogInSecretary(String username, String password)
        {
            return employeeAccountRepository.GetByUsernameAndPassword(username, password);
        }

        public EmployeeAccount LogInDirector(String username, String password)
        {
            return employeeAccountRepository.GetByUsernameAndPassword(username, password);
        }

    }
}