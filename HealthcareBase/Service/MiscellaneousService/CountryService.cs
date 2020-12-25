// File:    CountryService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CountryService

using System.Collections.Generic;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.GeneralitiesRepository;

namespace HealthcareBase.Service.MiscellaneousService
{
    public class CountryService
    {
        private readonly RepositoryWrapper<ICountryRepository> countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = new RepositoryWrapper<ICountryRepository>(countryRepository);
        }

        public Country GetByID(int id) => countryRepository.Repository.GetByID(id);

        public IEnumerable<Country> GetAll() => countryRepository.Repository.GetAll();
    }
}