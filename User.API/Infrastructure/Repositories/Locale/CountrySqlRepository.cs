using General;
using General.Repository;
using User.API.Infrastructure.Repositories.Locale.Interfaces;
using User.API.Model.Locale;

namespace User.API.Infrastructure.Repositories.Locale
{
    public class CountrySqlRepository:GenericSqlRepository<Country,int>,ICountryRepository
    {
        public CountrySqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}