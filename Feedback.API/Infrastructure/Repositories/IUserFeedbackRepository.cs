using Feedback.API.Model.Feedback;

namespace Feedback.API.Infrastructure.Repositories
{
    public interface IUserFeedbackRepository : IWrappableRepository<UserFeedback, int>
    {
    }
}