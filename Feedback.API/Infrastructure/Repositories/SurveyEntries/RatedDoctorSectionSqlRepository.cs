using Feedback.API.Model.Survey.SurveyEntry;
using General;
using General.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Feedback.API.Infrastructure.Repositories.SurveyEntries
{
    public class RatedDoctorSectionSqlRepository : GenericSqlRepository<DoctorSurveySection, int>, IRatedDoctorSectionRepository
    {
        public RatedDoctorSectionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<DoctorSurveySection> IncludeFields(IQueryable<DoctorSurveySection> query)
        {
            return query;//.Include(s => s.Doctor);
        }
    }
}