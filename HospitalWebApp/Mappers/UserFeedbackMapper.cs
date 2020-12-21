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
                isAnonymous = dto.IsAnonymous,
                isPublic = dto.IsPublic,
                isPublished = dto.IsPublished,
                UserComment = dto.UserComment,
                PatientAccountId = dto.UserId
            };
        }
    }
}