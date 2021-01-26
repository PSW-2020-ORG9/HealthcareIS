using Feedback.API.Model.Survey.SurveyEntry;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories
{
    public interface ISurveyResponseRepository : IWrappableRepository<RatedSurvey, int>
    {
    }
}