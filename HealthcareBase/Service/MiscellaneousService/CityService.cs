// File:    CityService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CityService

using System.Collections.Generic;
using Model.Users.Generalities;
using Repository.UsersRepository.GeneralitiesRepository;

namespace Service.MiscellaneousService
{
    public class CityService
    {
        private readonly CityRepository cityRepository;

        public CityService(CityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public City GetByID(int id)
        {
            return cityRepository.GetByID(id);
        }

        public IEnumerable<City> GetAll()
        {
            return cityRepository.GetAll();
        }

        public IEnumerable<City> GetByCountry(Country country)
        {
            return cityRepository.GetByCountry(country);
        }
    }
}