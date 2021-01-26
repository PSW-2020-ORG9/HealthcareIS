using Feedback.API.DTOs;
using Feedback.API.Model.Survey.SurveyEntry;
using System;

namespace Feedback.API.Mappers
{
    public static class SurveyResponseMapper
    {
        public static RatedSurvey DtoToObject(SurveyResponseDTO dto)
        {
            return new RatedSurvey(dto);
        }
    }
}
