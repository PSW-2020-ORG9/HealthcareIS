// File:    CountryService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CountryService

using General.Repository;
using System.Collections.Generic;
using User.API.Infrastructure.Repositories;
using User.API.Infrastructure.Repositories.Locale.Interfaces;
using User.API.Model.Locale;

namespace User.API.Services.LocaleServices
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