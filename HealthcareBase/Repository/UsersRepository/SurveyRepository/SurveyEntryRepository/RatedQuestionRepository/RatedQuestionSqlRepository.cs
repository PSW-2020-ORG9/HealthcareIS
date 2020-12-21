using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository
{
    public class RatedQuestionSqlRepository:GenericSqlRepository<RatedSurveyQuestion,int>,IRatedQuestionRepository
    {
        public RatedQuestionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

    }
}