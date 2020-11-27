using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedSectionRepository
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