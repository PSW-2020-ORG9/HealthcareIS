// File:    CityFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:16
// Purpose: Definition of Class CityFileRepository

using Model.CustomExceptions;
using Model.Users.Generalities;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.UsersRepository.GeneralitiesRepository
{
    public class CityFileRepository : GenericFileRepository<City, int>, CityRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private CountryRepository countryRepository;

        public CityFileRepository(CountryRepository countryRepository, String filePath) : base(filePath)
        {
            this.countryRepository = countryRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<City> GetByCountry(Country country)
        {
            return GetMatching(city => country.Equals(city.Country));
        }

        protected override int GenerateKey(City entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override City ParseEntity(City entity)
        {
            try
            {
                if (entity.Country != null)
                    entity.Country = countryRepository.GetByID(entity.Country.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}