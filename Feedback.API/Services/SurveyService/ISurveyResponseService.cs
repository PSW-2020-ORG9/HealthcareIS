﻿using System;
using System.Collections.Generic;
using Feedback.API.Model.Survey.SurveyEntry;

namespace Feedback.API.Services.SurveyService
{
    public interface ISurveyResponseService
    {
        RatedSurvey CreateSurveyResponse(RatedSurvey response);
        public RatedSurvey GetByExaminationId(int examinationId);
        public List<Boolean> ExistByExaminationIds(List<int> examinationIds);
    }
}
