using Feedback.API.Model.Survey;
using General;
using General.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Feedback.API.Infrastructure.Repositories
{
    public class SurveySqlRepository : GenericSqlRepository<Survey, int>, ISurveyRepository
    {
        public SurveySqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<Survey> IncludeFields(IQueryable<Survey> query)
        {
            return query.Include(s => s.SurveySections)
                .ThenInclude(ss => ss.SurveyQuestions);
        }
    }
    
}