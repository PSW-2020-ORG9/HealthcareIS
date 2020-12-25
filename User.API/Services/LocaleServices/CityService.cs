// File:    CityService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:05
// Purpose: Definition of Class CityService

using System.Collections.Generic;
using User.API.Infrastructure.Repositories;
using User.API.Model.Locale;
using ICityRepository = User.API.Infrastructure.Repositories.Locale.Interfaces.ICityRepository;

namespace User.API.Services.LocaleServices
{
    public class CityService
    {
        private readonly RepositoryWrapper<ICityRepository> cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = new RepositoryWrapper<ICityRepository>(cityRepository);
        }

        public City GetByID(int id) => cityRepository.Repository.GetByID(id);
        
        public IEnumerable<City> GetAll() => cityRepository.Repository.GetAll();
        
        public IEnumerable<City> GetByCountry(int countryId) => cityRepository.Repository.GetByCountry(countryId);
    }
}