// File:    CountryRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CountryRepository

using Model.Users.Generalities;
using Repository.Generics;

namespace Repository.UsersRepository.GeneralitiesRepository
{
    public interface CountryRepository : IWrappableRepository<Country, int>
    {
    }
}