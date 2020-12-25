using Feedback.API.Model.Survey.SurveyEntry;
using System;
using System.Collections.Generic;

namespace Feedback.API.Services
{
    public interface ISurveyResponseService
    {
        SurveyResponse CreateSurveyResponse(SurveyResponse response);
        public SurveyResponse GetByExaminationId(int examinationId);
        public List<Boolean> ExistByExaminationIds(List<int> examinationIds);
    }
}
