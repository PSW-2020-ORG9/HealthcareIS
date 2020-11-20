using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using Microsoft.EntityFrameworkCore;
using Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository
{
    public class RatedQuestionSqlRepository:GenericSqlRepository<RatedSurveyQuestion,int>,RatedQuestionRepository
    {
        public RatedQuestionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

    }
}