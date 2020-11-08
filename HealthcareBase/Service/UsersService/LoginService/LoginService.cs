// File:    LoginService.cs
// Author:  Lana
// Created: 28 May 2020 17:02:38
// Purpose: Definition of Class LoginService

using Model.Users.UserAccounts;
using Repository.Generics;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.UsersService.LoginService
{
    public class LoginService
    {
        private readonly RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository;
        private readonly RepositoryWrapper<PatientAccountRepository> patientAccountRepository;

        public LoginService(
            PatientAccountRepository patientAccountRepository,
            EmployeeAccountRepository employeeAccountRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<PatientAccountRepository>(patientAccountRepository);
            this.employeeAccountRepository =
                new RepositoryWrapper<EmployeeAccountRepository>(employeeAccountRepository);
        }

        public PatientAccount LogInPatient(string username, string password)
        {
            return patientAccountRepository.Repository.GetByUsernameAndPassword(username, password);
        }

        public EmployeeAccount LogInDoctor(string username, string password)
        {
            return employeeAccountRepository.Repository.GetByUsernameAndPassword(username, password);
        }

        public EmployeeAccount LogInSecretary(string username, string password)
        {
            return employeeAccountRepository.Repository.GetByUsernameAndPassword(username, password);
        }

        public EmployeeAccount LogInDirector(string username, string password)
        {
            return employeeAccountRepository.Repository.GetByUsernameAndPassword(username, password);
        }
    }
}