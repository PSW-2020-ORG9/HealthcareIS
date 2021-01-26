using Feedback.API.DTOs;
using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.RatedSurvey
{
    public class RatedSurvey : Entity<int>
    {
        public DateTime SubmittedAt { get;  }
        public int ExaminationId { get; }
        public int SurveyId { get; }
        public int PatientAccountId { get; }
        public List<RatedSurveyQuestion> Answers { get; }

        public RatedSurvey(RatedSurveyDTO dto) {
            SubmittedAt = DateTime.Now;
            SurveyId = dto.SurveyId;
            ExaminationId = dto.ExaminationId;
            PatientAccountId = dto.PatientAccountId;
            Answers = dto.Answers;
        }

        
    }
}