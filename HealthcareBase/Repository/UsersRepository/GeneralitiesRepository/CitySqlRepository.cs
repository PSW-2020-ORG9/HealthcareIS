using System.Collections.Generic;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.GeneralitiesRepository
{
    public class CitySqlRepository:GenericSqlRepository<City,int>,ICityRepository
    {
        public CitySqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public IEnumerable<City> GetByCountry(int countryId)
        {
            return GetMatching(city => city.CountryId == countryId);
        }
    }
}