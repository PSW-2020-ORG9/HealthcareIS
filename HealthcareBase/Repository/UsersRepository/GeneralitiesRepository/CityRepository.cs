// File:    CityRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CityRepository

using Model.Users.Generalities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.UsersRepository.GeneralitiesRepository
{
    public interface CityRepository : Repository<City, int>
    {
        IEnumerable<City> GetByCountry(Model.Users.Generalities.Country country);

    }
}