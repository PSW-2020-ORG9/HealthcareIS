// File:    LoginService.cs
// Author:  Lana
// Created: 28 May 2020 17:02:38
// Purpose: Definition of Class LoginService

using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;

namespace HealthcareBase.Service.UsersService.LoginService
{
    public class LoginService
    {
        private readonly RepositoryWrapper<IPatientAccountRepository> patientAccountRepository;

        public LoginService(
            IPatientAccountRepository patientAccountRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<IPatientAccountRepository>(patientAccountRepository);
        }

        public PatientAccount LogInPatient(string username, string password)
        {
            return patientAccountRepository.Repository.GetByUsernameAndPassword(username, password);
        }
    }
}