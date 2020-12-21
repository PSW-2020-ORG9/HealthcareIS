using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository
{
    public class SurveyResponseSqlRepository : GenericSqlRepository<SurveyResponse, int>, ISurveyResponseRepository
    {
        public SurveyResponseSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
