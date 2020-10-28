// File:    PatientRegistrationService.cs
// Author:  Lana
// Created: 28 May 2020 16:44:45
// Purpose: Definition of Class PatientRegistrationService

using System;
using Repository.UsersRepository.UserAccountsRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Model.CustomExceptions;

namespace Service.UsersService.PatientService
{
    public class PatientRegistrationService
    {
        private PatientAccountRepository patientAccountRepository;
        private PatientRepository patientRepository;

        public PatientRegistrationService(PatientAccountRepository patientAccountRepository, PatientRepository patientRepository)
        {
            this.patientAccountRepository = patientAccountRepository;
            this.patientRepository = patientRepository;
        }

        public Boolean IsRegistered(String jmbg)
        {
            return patientAccountRepository.ExistsByJMBG(jmbg); ;
        }

        public Boolean HasGuestAccount(String jmbg)
        {
            return patientRepository.ExistsByJMBG(jmbg);
        }

        public Patient GetGuestAccount(String jmbg)
        {
            return patientRepository.GetByJMBG(jmbg);
        }

        public PatientAccount Register(Patient patient, String username, String password)
        {
            if (patientAccountRepository.ExistsByJMBG(patient.Jmbg))
                throw new BadRequestException();

            if (!IsUsernameUnique(username))
                throw new NotUniqueException();
                
            if (HasGuestAccount(patient.Jmbg))
            {
                    patient=patientRepository.Update(patient);
            }
            else
            {
                   patient= patientRepository.Create(patient); 
            }

            PatientAccount newPatient = new PatientAccount();
            newPatient.Patient = patient;
            newPatient.Username = username;
            newPatient.Password = password;

                
            return  patientAccountRepository.Create(newPatient);
        }

        public Boolean IsUsernameUnique(String jmbg)
        {
            return patientAccountRepository.IsUsernameUnique(jmbg);
        }

    }
}