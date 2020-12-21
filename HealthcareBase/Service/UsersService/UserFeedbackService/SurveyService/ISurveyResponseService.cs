using HealthcareBase.Model.Users.Survey.SurveyEntry;
using System;
using System.Collections.Generic;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public interface ISurveyResponseService
    {
        SurveyResponse CreateSurveyResponse(SurveyResponse response);
        public SurveyResponse GetByExaminationId(int examinationId);
        public List<Boolean> ExistByExaminationIds(List<int> examinationIds);
    }
}
