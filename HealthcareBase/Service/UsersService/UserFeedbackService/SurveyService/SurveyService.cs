using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public class SurveyService:ISurveyService
    {
        private readonly RepositoryWrapper<ISurveyRepository> surveyRepository;

        public SurveyService(ISurveyRepository repository)
        {
            surveyRepository=new RepositoryWrapper<ISurveyRepository>(repository);
        }
        /// <summary>
        /// Gets predefinded survey object according to passed surveyId.
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public Survey GetById(int surveyId) => surveyRepository.Repository.GetByID(surveyId);
    }
}