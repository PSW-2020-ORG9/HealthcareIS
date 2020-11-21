// File:    EmployeeFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class EmployeeFileRepository

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.Generalities;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.GeneralitiesRepository;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class EmployeeFileRepository : GenericFileRepository<Employee, int>, EmployeeRepository
    {
        private readonly CityRepository cityRepository;
        private readonly CountryRepository countryRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public EmployeeFileRepository(CityRepository cityRepository, CountryRepository countryRepository,
            string filePath) : base(filePath)
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
                if (entity.Person.Citizenships != null)
                {
                    var citizenship = new List<Country>();
                    foreach (var country in entity.Person.Citizenships)
                        citizenship.Add(countryRepository.GetByID(country.GetKey()));
                }

                if (entity.Person.CityOfResidence != null)
                    entity.Person.CityOfResidence = cityRepository.GetByID(entity.Person.CityOfResidence.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}