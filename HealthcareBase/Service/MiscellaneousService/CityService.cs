// File:    CityService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CityService

using System.Collections.Generic;
using Model.Users.Generalities;
using Repository.Generics;
using Repository.UsersRepository.GeneralitiesRepository;

namespace Service.MiscellaneousService
{
    public class CityService
    {
        private readonly RepositoryWrapper<CityRepository> cityRepository;

        public CityService(CityRepository cityRepository)
        {
            this.cityRepository = new RepositoryWrapper<CityRepository>(cityRepository);
        }

        public City GetByID(int id)
        {
            return cityRepository.Repository.GetByID(id);
        }

        public IEnumerable<City> GetAll()
        {
            return cityRepository.Repository.GetAll();
        }

        public IEnumerable<City> GetByCountry(Country country)
        {
            return cityRepository.Repository.GetByCountry(country);
        }
    }
}