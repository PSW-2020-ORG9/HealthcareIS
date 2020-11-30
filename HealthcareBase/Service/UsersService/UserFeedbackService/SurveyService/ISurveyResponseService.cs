using HealthcareBase.Model.Users.Survey.SurveyEntry;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService
{
    public interface ISurveyResponseService
    {
        SurveyResponse CreateSurveyResponse(SurveyResponse response);
    }
}
