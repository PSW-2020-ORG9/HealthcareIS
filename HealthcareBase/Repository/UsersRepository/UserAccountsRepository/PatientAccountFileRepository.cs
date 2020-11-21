// File:    PatientAccountFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class PatientAccountFileRepository

using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public class PatientAccountFileRepository : GenericFileRepository<PatientAccount, int>, PatientAccountRepository
    {
        private readonly DoctorRepository doctorRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly PatientRepository patientRepository;

        public PatientAccountFileRepository(PatientRepository patientRepository, DoctorRepository doctorRepository,
            string filePath) : base(filePath)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public bool ExistsByJMBG(string jmbg)
        {
            foreach (var currentPatient in GetAll())
                if (currentPatient.Patient.Jmbg.Equals(jmbg))
                    return true;
            return false;
        }


        public bool IsUsernameUnique(string username)
        {
            foreach (var currentPatientAccount in GetAll())
                if (currentPatientAccount.Username.Equals(username))
                    return false;


            return true;
        }

        public string GetPasswordByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public PatientAccount GetByUsernameAndPassword(string username, string password)
        {
            foreach (var currentPatientAccount in GetAll())
                if (currentPatientAccount.Username.Equals(username) && currentPatientAccount.Password.Equals(password))
                    return currentPatientAccount;

            throw new BadReferenceException();
        }

        public PatientAccount GetByPatient(Patient patient)
        {
            var found = GetMatching(account => account.Patient.Equals(patient));
            if (found.Count() == 0)
                throw new BadRequestException();
            return found.ToList()[0];
        }

        protected override PatientAccount ParseEntity(PatientAccount entity)
        {
            try
            {
                if (entity.Patient != null)
                    entity.Patient = patientRepository.GetByID(entity.Patient.GetKey());

                if (entity.FavouriteDoctors != null)
                {
                    var favouriteDoctors = new List<Doctor>();
                    foreach (var country in entity.FavouriteDoctors)
                        favouriteDoctors.Add(doctorRepository.GetByID(country.GetKey()));
                    entity.FavouriteDoctors = favouriteDoctors;
                }
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(PatientAccount entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}