// File:    PatientRegistrationService.cs
// Author:  Lana
// Created: 28 May 2020 16:44:45
// Purpose: Definition of Class PatientRegistrationService

using Model.CustomExceptions;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.UsersService.PatientService
{
    public class PatientRegistrationService
    {
        private readonly RepositoryWrapper<PatientAccountRepository> patientAccountRepository;
        private readonly RepositoryWrapper<PatientRepository> patientRepository;

        public PatientRegistrationService(
            PatientAccountRepository patientAccountRepository,
            PatientRepository patientRepository)
        {
            this.patientAccountRepository = new RepositoryWrapper<PatientAccountRepository>(patientAccountRepository);
            this.patientRepository = new RepositoryWrapper<PatientRepository>(patientRepository);
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
            if (patientAccountRepository.Repository.ExistsByJMBG(patient.Jmbg))
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new NotUniqueException();

            if (HasGuestAccount(patient.Jmbg))
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