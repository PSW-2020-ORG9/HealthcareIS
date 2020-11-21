using System.Linq;
using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.UsersRepository.UserFeedbackRepository
{
    public class UserFeedbackSqlRepository : GenericSqlRepository<UserFeedback, int>, UserFeedbackRepository
    {
        public UserFeedbackSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }


        public override IQueryable<UserFeedback> IncludeFields(IQueryable<UserFeedback> query) 
        {
            return query.Include(uf => uf.User);
                
            
        }
    }
}
