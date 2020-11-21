// File:    CountryRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CountryRepository

using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.GeneralitiesRepository
{
    public interface CountryRepository : IWrappableRepository<Country, int>
    {
    }
}