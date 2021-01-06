using System;
using System.Collections.Generic;
using Feedback.API.Model.Survey.SurveyEntry;

namespace Feedback.API.Services.SurveyService
{
    public interface ISurveyResponseService
    {
        SurveyResponse CreateSurveyResponse(SurveyResponse response);
        public SurveyResponse GetByExaminationId(int examinationId);
        public List<Boolean> ExistByExaminationIds(List<int> examinationIds);
    }
}
