using Feedback.API.Model.Feedback;
using System.Linq;

namespace Feedback.API.Infrastructure.Repositories
{
    public class UserFeedbackSqlRepository : GenericSqlRepository<UserFeedback, int>, IUserFeedbackRepository
    {
        public UserFeedbackSqlRepository(IContextFactory contextFactory) : base(contextFactory) { }

        protected override IQueryable<UserFeedback> IncludeFields(IQueryable<UserFeedback> query) 
        {
            return query;//.Include(uf => uf.PatientAccount);
        }
    }
}
