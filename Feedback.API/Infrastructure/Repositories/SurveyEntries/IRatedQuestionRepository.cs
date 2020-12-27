using Feedback.API.Model.Survey.SurveyEntry;
using General.Repository;

namespace Feedback.API.Infrastructure.Repositories.SurveyEntries
{
    public interface IRatedQuestionRepository : IWrappableRepository<RatedSurveyQuestion, int>
    {
        
    }
}