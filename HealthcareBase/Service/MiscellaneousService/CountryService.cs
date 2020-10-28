// File:    CountryService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CountryService

using Model.Users.Generalities;
using Repository.UsersRepository.GeneralitiesRepository;
using System;
using System.Collections.Generic;

namespace Service.MiscellaneousService
{
    public class CountryService
    {
        private CountryRepository countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public Country GetByID(int id)
        {
            return countryRepository.GetByID(id);
        }

        public IEnumerable<Country> GetAll()
        {
            return countryRepository.GetAll();
        }

    }
}