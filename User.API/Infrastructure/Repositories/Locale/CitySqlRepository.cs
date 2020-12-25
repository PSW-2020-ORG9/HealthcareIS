using System.Collections.Generic;
using User.API.Infrastructure.Repositories.Locale.Interfaces;
using User.API.Model.Locale;

namespace User.API.Infrastructure.Repositories.Locale
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