// File:    CountryRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:47:14
// Purpose: Definition of Interface CountryRepository


using General.Repository;
using User.API.Model.Locale;

namespace User.API.Infrastructure.Repositories.Locale.Interfaces
{
    public interface ICountryRepository : IWrappableRepository<Country, int>
    {
    }
}