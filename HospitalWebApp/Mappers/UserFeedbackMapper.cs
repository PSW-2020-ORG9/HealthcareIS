using System;
using HealthcareBase.Model.Users.UserFeedback;
using HospitalWebApp.Dtos;

namespace HospitalWebApp.Mappers
{
    public static class UserFeedbackMapper
    {
        public static UserFeedback DtoToObject(UserFeedbackDto dto)
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