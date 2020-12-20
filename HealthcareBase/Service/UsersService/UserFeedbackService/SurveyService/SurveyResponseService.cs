using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly RepositoryWrapper<ISurveyResponseRepository> _surveyResponseWrapper;
        public SurveyResponseService(ISurveyResponseRepository surveyResponseRepository)
        {
            _surveyResponseWrapper = new RepositoryWrapper<ISurveyResponseRepository>(surveyResponseRepository);
        }
        public SurveyResponse CreateSurveyResponse(SurveyResponse response)
        {
            return _surveyResponseWrapper.Repository.Create(response);
        }

        public SurveyResponse GetByExaminationId(int examinationId)
            => _surveyResponseWrapper.Repository
                .GetMatching(survey => survey.ExaminationId == examinationId)
                .FirstOrDefault();

        public List<bool> ExistByExaminationIds(List<int> examinationIds)
        {
            List<bool> retVal = new List<bool>();
            examinationIds.ForEach(id =>
            {
                retVal.Add(GetByExaminationId(id) != default) ;
            });
            return retVal;
        }
    }
}
