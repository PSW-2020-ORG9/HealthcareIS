using Feedback.API.Model.Survey.SurveyEntry;
using General;
using General.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Feedback.API.Infrastructure.Repositories.SurveyEntries
{
    public class RatedSectionSqlRepository : GenericSqlRepository<RatedSurveySection, int>, IRatedSectionRepository
    {
        public RatedSectionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
            
        }

        protected override IQueryable<RatedSurveySection> IncludeFields(IQueryable<RatedSurveySection> query)
        {
            return query.Include(rss => rss.RatedSurveyQuestions)
                .ThenInclude(qs => qs.SurveyQuestion);
        }
    }
}