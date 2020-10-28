// File:    PatientFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class PatientFileRepository

using Model.Users.Patient;
using Repository.Generics;
using System;
using System.Collections.Generic;
using Model.CustomExceptions;
using Repository.UsersRepository.GeneralitiesRepository;
using Model.Users.Generalities;
using Model.Utilities;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class PatientFileRepository : GenericFileRepository<Patient, int>, PatientRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private CityRepository cityRepository;
        private CountryRepository countryRepository;

        public PatientFileRepository(CityRepository cityRepository, CountryRepository countryRepository, String filePath) : base(filePath)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public Boolean ExistsByJMBG(String jmbg)
        {
            List<Patient> patients = (List<Patient>)GetAll();

            foreach (Patient currentPatient in patients)
            {
                if (currentPatient.Jmbg.Equals(jmbg))
                    return true;

            }
            return false;
        }

        public Patient GetByJMBG(String jmbg)
        {

            foreach (Patient currentPatient in GetAll())
            {
                if (currentPatient.Jmbg.Equals(jmbg))
                    return currentPatient;

            }

            throw new BadRequestException();
        }

        protected override Patient ParseEntity(Patient entity)
        {
            try
            {
                if (entity.Citizenship != null)
                {
                    List<Country> citizenship = new List<Country>();
                    foreach (Country country in entity.Citizenship)
                    {
                        citizenship.Add(countryRepository.GetByID(country.GetKey()));
                    }
                    entity.Citizenship = citizenship;
                }

                if (entity.CityOfBirth != null)
                {
                    entity.CityOfBirth = cityRepository.GetByID(entity.CityOfBirth.GetKey());
                }
                if (entity.CityOfResidence != null)
                {
                    entity.CityOfResidence = cityRepository.GetByID(entity.CityOfResidence.GetKey());
                }

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