using Feedback.API.DTOs;
using Feedback.API.Model.Feedback.Domain.AggregatesModel.SurveyAggregate.RatedSurvey;
using System;

namespace Feedback.API.Mappers
{
    public static class SurveyResponseMapper
    {
        public static RatedSurvey DtoToObject(RatedSurveyDTO dto)
        {
            return new RatedSurvey(dto);
        }
    }
}
