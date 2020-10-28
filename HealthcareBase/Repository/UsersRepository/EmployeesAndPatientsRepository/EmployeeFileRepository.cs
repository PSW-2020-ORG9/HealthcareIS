// File:    EmployeeFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class EmployeeFileRepository

using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.Generalities;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.GeneralitiesRepository;
using System;
using System.Collections.Generic;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class EmployeeFileRepository : GenericFileRepository<Employee, int>, EmployeeRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private CityRepository cityRepository;
        private CountryRepository countryRepository;

        public EmployeeFileRepository(CityRepository cityRepository, CountryRepository countryRepository, String filePath) : base(filePath)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Employee entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Employee ParseEntity(Employee entity)
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
    }
}