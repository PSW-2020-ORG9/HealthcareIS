using Feedback.API.Model.Survey;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories
{
    public interface ISurveyRepository : IWrappableRepository<Survey, int>
    {
        
    }
}