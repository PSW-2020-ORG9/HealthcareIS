// File:    PatientRegistrationService.cs
// Author:  Lana
// Created: 28 May 2020 16:44:45
// Purpose: Definition of Class PatientRegistrationService

using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;

namespace HealthcareBase.Service.UsersService.PatientService
{
    public class PatientRegistrationService
    {
        private readonly RepositoryWrapper<IPatientAccountRepository> patientAccountRepository;
        private readonly RepositoryWrapper<IPatientRepository> patientRepository;

        public PatientRegistrationService(
            IPatientAccountRepository patientAccountRepository,
            IPatientRepository patientRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<IPatientAccountRepository>(patientAccountRepository);
            this.patientRepository = new RepositoryWrapper<IPatientRepository>(patientRepository);
        }

        public bool IsRegistered(string jmbg)
        {
            return patientAccountRepository.Repository.ExistsByJMBG(jmbg);
            ;
        }

        public bool HasGuestAccount(string jmbg)
        {
            return patientRepository.Repository.ExistsByJMBG(jmbg);
        }

        public Patient GetGuestAccount(string jmbg)
        {
            return patientRepository.Repository.GetByJMBG(jmbg);
        }

        public PatientAccount Register(Patient patient, string username, string password)
        {
            if (patientAccountRepository.Repository.ExistsByJMBG(patient.Person.Jmbg))
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new NotUniqueException();

            if (HasGuestAccount(patient.Person.Jmbg))
                patient = patientRepository.Repository.Update(patient);
            else
                patient = patientRepository.Repository.Create(patient);

            var newPatient = new PatientAccount();
            newPatient.Patient = patient;
            newPatient.Username = username;
            newPatient.Password = password;


            return patientAccountRepository.Repository.Create(newPatient);
        }

        public bool IsUsernameUnique(string jmbg)
        {
            return patientAccountRepository.Repository.IsUsernameUnique(jmbg);
        }
    }
}