// File:    CountyFileRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:17
// Purpose: Definition of Class CountyFileRepository

using Model.Users.Generalities;
using Model.Utilities;
using Repository.Generics;

namespace Repository.UsersRepository.GeneralitiesRepository
{
    public class CountryFileRepository : GenericFileRepository<Country, int>, CountryRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public CountryFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Country entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Country ParseEntity(Country entity)
        {
            return entity;
        }
    }
}