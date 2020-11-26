using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedDotorSectionRepository
{
    public class RatedDoctorSectionSqlRepository:GenericSqlRepository<DoctorSurveySection,int>,RatedDoctorSectionRepository
    {
        public RatedDoctorSectionSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        protected override IQueryable<DoctorSurveySection> IncludeFields(IQueryable<DoctorSurveySection> query)
        {
            return query.Include(s => s.Doctor);
        }
    }
}