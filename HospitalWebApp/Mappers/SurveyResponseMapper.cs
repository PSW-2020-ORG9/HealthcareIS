using HealthcareBase.Model.Users.Survey.DTOs;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using System;

namespace HospitalWebApp.Mappers
{
    public class SurveyResponseMapper
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
