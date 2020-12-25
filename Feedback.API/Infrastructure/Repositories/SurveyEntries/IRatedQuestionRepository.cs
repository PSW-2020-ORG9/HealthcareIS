using Feedback.API.Model.Survey.SurveyEntry;

namespace Feedback.API.Infrastructure.Repositories.SurveyEntries
{
    public interface IRatedQuestionRepository : IWrappableRepository<RatedSurveyQuestion, int>
    {
        
    }
}