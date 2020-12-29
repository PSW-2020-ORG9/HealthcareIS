// File:    CityRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CityRepository

using General.Repository;
using System.Collections.Generic;
using User.API.Model.Locale;

namespace User.API.Infrastructure.Repositories.Locale.Interfaces
{
    public interface ICityRepository : IWrappableRepository<City, int>
    {
        IEnumerable<City> GetByCountry(int countryId);
        
    }
}