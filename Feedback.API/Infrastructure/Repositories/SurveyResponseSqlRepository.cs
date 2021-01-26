using Feedback.API.Model.Survey.SurveyEntry;
using General;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories
{
    public class SurveyResponseSqlRepository : GenericSqlRepository<RatedSurvey, int>, ISurveyResponseRepository
    {
        public SurveyResponseSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
