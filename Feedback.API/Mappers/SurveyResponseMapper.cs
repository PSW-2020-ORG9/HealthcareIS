using Feedback.API.DTOs;
using Feedback.API.Model.Survey.SurveyEntry;
using System;

namespace Feedback.API.Mappers
{
    public static class SurveyResponseMapper
    {
        public static SurveyResponse DtoToObject(SurveyResponseDTO dto)
        {
            return new SurveyResponse
            {
                SubmittedAt = DateTime.Now,
                SurveyId = dto.SurveyId,
                RatedSurveySections = dto.RatedSurveySections,
                DoctorSurveySection = dto.DoctorSurveySection,
                PatientAccountId = dto.PatientAccountId,
                ExaminationId = dto.ExaminationId
            };
        }
    }
}
