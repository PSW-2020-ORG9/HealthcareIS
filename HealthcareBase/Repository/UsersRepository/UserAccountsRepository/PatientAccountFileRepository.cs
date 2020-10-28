// File:    PatientAccountFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class PatientAccountFileRepository

using Model.CustomExceptions;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;
using System.Collections.Generic;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Model.Users.Employee;

using System.Linq;
using Model.Utilities;

namespace Repository.UsersRepository.UserAccountsRepository
{
    public class PatientAccountFileRepository : GenericFileRepository<PatientAccount, int>, PatientAccountRepository
    {
        private PatientRepository patientRepository;
        private DoctorRepository doctorRepository;
        private IntegerKeyGenerator keyGenerator;

        public PatientAccountFileRepository(PatientRepository patientRepository, DoctorRepository doctorRepository, 
            String filePath) : base(filePath)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public Boolean ExistsByJMBG(String jmbg)
        {

            foreach (PatientAccount currentPatient in GetAll())
            {
                if (currentPatient.Patient.Jmbg.Equals(jmbg))
                    return true;

            }
            return false;
        }


        public Boolean IsUsernameUnique(String username)
        {

            foreach (PatientAccount currentPatientAccount in GetAll())
            {
                if (currentPatientAccount.Username.Equals(username))
                    return false;
            }


            return true;
        }

        public String GetPasswordByUsername(String username)
        {
            throw new NotImplementedException();
        }

        public PatientAccount GetByUsernameAndPassword(String username, String password)
        {
       

            foreach (PatientAccount currentPatientAccount in GetAll())
            {
                if (currentPatientAccount.Username.Equals(username) && currentPatientAccount.Password.Equals(password))
                    return currentPatientAccount;

            }

            throw new BadReferenceException();
        }

        protected override PatientAccount ParseEntity(PatientAccount entity)
        {

            try
            {
                if (entity.Patient != null)
                    entity.Patient = patientRepository.GetByID(entity.Patient.GetKey());

                if (entity.FavouriteDoctors != null)
                {
                    List<Doctor> favouriteDoctors = new List<Doctor>();
                    foreach (Doctor country in entity.FavouriteDoctors)
                    {
                        favouriteDoctors.Add(doctorRepository.GetByID(country.GetKey()));
                    }
                    entity.FavouriteDoctors = favouriteDoctors;
                }

            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        public PatientAccount GetByPatient(Patient patient)
        {
            IEnumerable<PatientAccount> found = GetMatching(account => account.Patient.Equals(patient));
            if (found.Count() == 0)
                throw new BadRequestException();
            return found.ToList()[0];
        }

        protected override int GenerateKey(PatientAccount entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}