using Feedback.API.DTOs;
using Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate;
using Feedback.API.Infrastructure;
using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey.SurveyEntry
{
    public class SurveyResponse : Entity<int>
    {
        public DateTime SubmittedAt { get; set; }
        public int ExaminationId { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<SurveySection> RatedSurveySections { get; set; }
        public int? DoctorSurveySectionId { get; set; }
        public DoctorSurveySection DoctorSurveySection { get; set; }
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }

        public SurveyResponse() { }
        public SurveyResponse(SurveyResponseDTO dto) {
            SubmittedAt = DateTime.Now;
            SurveyId = dto.SurveyId;
            RatedSurveySections = dto.RatedSurveySections;
            DoctorSurveySection = dto.DoctorSurveySection;
            PatientAccountId = dto.PatientAccountId;
            ExaminationId = dto.ExaminationId;
        }

        
    }
}