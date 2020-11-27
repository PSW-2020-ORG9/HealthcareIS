using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository
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