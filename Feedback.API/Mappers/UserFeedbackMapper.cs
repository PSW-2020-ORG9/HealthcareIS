using Feedback.API.DTOs;
using Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate;
using System;

namespace Feedback.API.Mappers
{
    public static class UserFeedbackMapper
    {
        public static UserFeedback DtoToObject(UserFeedbackDTO dto)
        {
            FeedbackVisibility fv = new FeedbackVisibility(dto.IsAnonymous, dto.IsPublic, dto.IsPublished);
            return new UserFeedback(DateTime.Now, dto.UserComment, fv, dto.UserId);
        }
    }
}