using Feedback.API.Model.Survey.SurveyEntry;

namespace Feedback.API.Infrastructure.Repositories.SurveyEntries
{
    public class RatedQuestionSqlRepository:GenericSqlRepository<RatedSurveyQuestion, int>, IRatedQuestionRepository
    {
        public RatedQuestionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

    }
}