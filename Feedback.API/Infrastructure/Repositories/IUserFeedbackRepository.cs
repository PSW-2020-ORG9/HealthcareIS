using Feedback.API.Model.Feedback;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories
{
    public interface IUserFeedbackRepository : IWrappableRepository<UserFeedback, int>
    {
    }
}