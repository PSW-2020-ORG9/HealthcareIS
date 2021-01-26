using Feedback.API.DTOs;
using Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate;
using Feedback.API.Infrastructure;
using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class RatedSurvey : Entity<int>
    {
        public DateTime SubmittedAt { get;  }
        public int ExaminationId { get; }
        public int SurveyId { get; }
        public int PatientAccountId { get; }
        public List<RatedSurveyQuestion> Answers { get; }

        public RatedSurvey(int examinationId, int surveyId, int patientId, List<RatedSurveyQuestion> answers) {
            SubmittedAt = DateTime.Now;
            SurveyId = surveyId;
            ExaminationId = examinationId;
            PatientAccountId = patientId;
            Answers = answers;
        }

        
    }
}