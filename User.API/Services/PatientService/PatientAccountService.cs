// File:    PatientAccountService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientAccountService

using System;
using System.Linq;
using User.API.Infrastructure.Repositories;
using User.API.Infrastructure.Repositories.Users.UserAccounts;
using User.API.Model.Users.Patients;
using User.API.Model.Users.UserAccounts;


namespace User.API.Services.PatientService
{
    public class PatientAccountService : IPatientAccountService
    {
        private readonly RepositoryWrapper<IPatientAccountRepository> patientAccountRepository;

        public PatientAccountService(
            IPatientAccountRepository patientAccountRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<IPatientAccountRepository>(patientAccountRepository);
            
        }

        public PatientAccount CreateAccount(PatientAccount patientAccount)
        {
            return patientAccountRepository.Repository.Create(patientAccount);
        }
        public void DeleteAccount(PatientAccount patientAccount)
        {
            patientAccountRepository.Repository.Delete(patientAccount);
        }

        public PatientAccount GetAccount(Patient patient)
        {
            return patientAccountRepository.Repository.GetByPatient(patient);
        }

        public PatientAccount GetAccount(int patientId)
        {
            return patientAccountRepository.Repository.GetByPatientId(patientId);
        }

        public PatientAccount ChangePassword(PatientAccount account, string newPassword)
        {
            
            var acc = patientAccountRepository.Repository.GetByID(account.Id);
            acc.Credentials = acc.Credentials.ChangePassword(newPassword);
            return patientAccountRepository.Repository.Update(acc);
        }
        public void ActivateAccount(Guid guid)
        {
           var patientAccount = patientAccountRepository.Repository
                                .GetMatching(p => p.UserGuid == guid)
                                .First();
           patientAccount.ActivateAccount();
           patientAccountRepository.Repository.Update(patientAccount);

        }
    }
}