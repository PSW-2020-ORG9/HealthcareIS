using System;
using HospitalWebApp.Dtos;
using Model.CustomExceptions;
using Model.Users.UserFeedback;

namespace HealthcareBase.Service.ValidationService
{
    public static class UserFeedbackValidator
    {
        private const int COMMENT_MAX_LEN = 300;

        /// <summary>
        /// Validates passed UserFeedback object.
        /// </summary>
        /// <param name="entity"></param>
        public static void validate(UserFeedbackDto entity)
        {
            CheckFields(entity);
        }

        /// <summary>
        /// Validates object fields content.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ValidationException"></exception>
        private static void CheckFields(UserFeedbackDto entity)
        {
            IsCommentEmpty(entity.UserComment);
            IsCommentTooLong(entity.UserComment);
        }

        /// <summary>
        /// Checks if comment is empty
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ValidationException"></exception>
        private static void IsCommentEmpty(string comment)
        {
            if (comment.Trim().Equals(""))
                throw new ValidationException("User comment cannot be empty.");
        }

        /// <summary>
        /// Checks if comment is too long.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ValidationException"></exception>
        private static void IsCommentTooLong(string comment)
        {
            if (comment.Length > COMMENT_MAX_LEN)
                throw new ValidationException(message: $"User comment is longer than {COMMENT_MAX_LEN} characters.");
        }
    }
}