using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using Repository.Generics;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public class SurveyService:ISurveyService
    {
        private readonly RepositoryWrapper<SurveyRepository> surveyRepository;

        public SurveyService(SurveyRepository repository)
        {
            surveyRepository=new RepositoryWrapper<SurveyRepository>(repository);
        }

        public Survey GetById(int surveyId) => surveyRepository.Repository.GetByID(surveyId);
    }
}