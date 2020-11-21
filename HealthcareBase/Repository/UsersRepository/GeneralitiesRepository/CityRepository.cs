// File:    CityRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CityRepository

using System.Collections.Generic;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.GeneralitiesRepository
{
    public interface CityRepository : IWrappableRepository<City, int>
    {
        IEnumerable<City> GetByCountry(Country country);
    }
}