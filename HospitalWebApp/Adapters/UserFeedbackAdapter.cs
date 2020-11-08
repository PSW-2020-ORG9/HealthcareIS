using System;
using HospitalWebApp.Dtos;
using Model.Users.UserFeedback;

namespace HospitalWebApp.Adapters
{
    public class UserFeedbackAdapter
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
                UserId = dto.UserId
            };
        }
    }
}