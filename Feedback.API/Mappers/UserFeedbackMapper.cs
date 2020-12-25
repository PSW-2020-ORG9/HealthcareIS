using Feedback.API.DTOs;
using Feedback.API.Model.Feedback;
using System;

namespace Feedback.API.Mappers
{
    public static class UserFeedbackMapper
    {
        public static UserFeedback DtoToObject(UserFeedbackDTO dto)
        {
            return new UserFeedback
            {
                Date = DateTime.Now,
                FeedbackVisibility = new FeedbackVisibility
                {
                    IsAnonymous = dto.IsAnonymous,
                    IsPublic = dto.IsPublic,
                    IsPublished = dto.IsPublished
                },
                UserComment = dto.UserComment,
                PatientAccountId = dto.UserId
            };
        }
    }
}