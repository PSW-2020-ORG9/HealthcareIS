using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.GeneralitiesRepository
{
    public class CountrySqlRepository:GenericSqlRepository<Country,int>,ICountryRepository
    {
        public CountrySqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}