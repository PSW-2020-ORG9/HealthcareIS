using Feedback.API.Model.Survey.SurveyEntry;
using General;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories.SurveyEntries
{
    public class RatedQuestionSqlRepository: GenericSqlRepository<RatedSurveyQuestion, int>, IRatedQuestionRepository
    {
        public RatedQuestionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

    }
}