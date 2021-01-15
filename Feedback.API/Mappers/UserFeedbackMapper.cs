using Feedback.API.DTOs;
using Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate;
using System;

namespace Feedback.API.Mappers
{
    public static class UserFeedbackMapper
    {
        public static UserFeedback DtoToObject(UserFeedbackDTO dto)
        {
            return new UserFeedback(dto);
        }
    }
}