using Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories
{
    public interface IUserFeedbackRepository : IWrappableRepository<UserFeedback, int>
    {
    }
}