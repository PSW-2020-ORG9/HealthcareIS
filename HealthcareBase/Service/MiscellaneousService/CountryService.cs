// File:    CountryService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CountryService

using System.Collections.Generic;
using Model.Users.Generalities;
using Repository.Generics;
using Repository.UsersRepository.GeneralitiesRepository;

namespace Service.MiscellaneousService
{
    public class CountryService
    {
        private readonly RepositoryWrapper<CountryRepository> countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = new RepositoryWrapper<CountryRepository>(countryRepository);
        }

        public Country GetByID(int id)
        {
            return countryRepository.Repository.GetByID(id);
        }

        public IEnumerable<Country> GetAll()
        {
            return countryRepository.Repository.GetAll();
        }
    }
}