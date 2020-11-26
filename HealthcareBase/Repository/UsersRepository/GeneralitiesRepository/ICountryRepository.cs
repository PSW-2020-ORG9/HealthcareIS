// File:    CountryRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CountryRepository

using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.GeneralitiesRepository
{
    public interface ICountryRepository : IWrappableRepository<Country, int>
    {
    }
}