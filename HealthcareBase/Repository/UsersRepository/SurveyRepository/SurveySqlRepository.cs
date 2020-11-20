using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Survey;
using Microsoft.EntityFrameworkCore;
using Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.SurveyRepository
{
    public class SurveySqlRepository : GenericSqlRepository<Survey, int>, SurveyRepository
    {
        public SurveySqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public override IQueryable<Survey> IncludeFields(IQueryable<Survey> query)
        {
            return query.Include(s => s.SurveySections)
                .ThenInclude(ss => ss.SurveyQuestions);

        }
    }
    
}