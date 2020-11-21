// File:    PatientFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class PatientFileRepository

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.GeneralitiesRepository;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class PatientFileRepository : GenericFileRepository<Patient, int>, PatientRepository
    {
        private readonly CityRepository cityRepository;
        private readonly CountryRepository countryRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public PatientFileRepository(CityRepository cityRepository, CountryRepository countryRepository,
            string filePath) : base(filePath)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public bool ExistsByJMBG(string jmbg)
        {
            var patients = (List<Patient>) GetAll();

            foreach (var currentPatient in patients)
                if (currentPatient.Jmbg.Equals(jmbg))
                    return true;
            return false;
        }

        public Patient GetByJMBG(string jmbg)
        {
            foreach (var currentPatient in GetAll())
                if (currentPatient.Jmbg.Equals(jmbg))
                    return currentPatient;

            throw new BadRequestException();
        }

        protected override Patient ParseEntity(Patient entity)
        {
            try
            {
                if (entity.Citizenship != null)
                {
                    var citizenship = new List<Country>();
                    foreach (var country in entity.Citizenship)
                        citizenship.Add(countryRepository.GetByID(country.GetKey()));
                    entity.Citizenship = citizenship;
                }

                if (entity.CityOfBirth != null)
                    entity.CityOfBirth = cityRepository.GetByID(entity.CityOfBirth.GetKey());
                if (entity.CityOfResidence != null)
                    entity.CityOfResidence = cityRepository.GetByID(entity.CityOfResidence.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(Patient entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}