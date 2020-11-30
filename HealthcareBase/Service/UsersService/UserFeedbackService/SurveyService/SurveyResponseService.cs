using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly RepositoryWrapper<ISurveyResponseRepository> _surveyResponseRepository;
        public SurveyResponseService(ISurveyResponseRepository surveyResponseRepository)
        {
            _surveyResponseRepository = new RepositoryWrapper<ISurveyResponseRepository>(surveyResponseRepository);
        }
        public SurveyResponse CreateSurveyResponse(SurveyResponse response)
        {
            return _surveyResponseRepository.Repository.Create(response);
        }
    }
}
