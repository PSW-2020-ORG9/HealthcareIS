using Feedback.API.DTOs;
using Feedback.API.Model.Survey.SurveyEntry;
using System;

namespace Feedback.API.Mappers
{
    public static class SurveyResponseMapper
    {
        public static SurveyResponse DtoToObject(SurveyResponseDTO dto)
        {
            return new SurveyResponse(dto);
        }
    }
}
