using Feedback.API.Infrastructure.Repositories;
using Feedback.API.Model.Survey;
using General.Repository;

namespace Feedback.API.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly RepositoryWrapper<ISurveyRepository> surveyRepository;

        public SurveyService(ISurveyRepository repository)
        {
            surveyRepository = new RepositoryWrapper<ISurveyRepository>(repository);
        }
        public Survey GetById(int surveyId) => surveyRepository.Repository.GetByID(surveyId);
    }
}