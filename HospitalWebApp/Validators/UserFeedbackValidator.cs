using System;
using HealthcareBase.Model.CustomExceptions;
using HospitalWebApp.Dtos;

namespace HospitalWebApp.Validators
{
    public static class UserFeedbackValidator
    {
        private const int COMMENT_MAX_LEN = 300;

        /// <summary>
        /// Validates the given <see cref="UserFeedbackDto"/> object.
        /// </summary>
        /// <param name="entity"> DTO to be validated </param>
        /// <exception cref="ValidationException">
        /// If comment is empty, or longer than <see cref="COMMENT_MAX_LEN"/>
        /// </exception>
        public static void Validate(UserFeedbackDto entity)
        {
            IsCommentEmpty(entity.UserComment);
            IsCommentTooLong(entity.UserComment);
        }

        /// <summary>
        /// Checks if comment is empty
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="ValidationException">
        /// If the given string is 
        /// </exception>
        private static void IsCommentEmpty(string comment)
        {
            if (comment.Trim().Equals(""))
                throw new ValidationException("User comment cannot be empty.");
        }

        /// <summary>
        /// Checks if comment is too long.
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="ValidationException"></exception>
        private static void IsCommentTooLong(string comment)
        {
            if (comment.Length > COMMENT_MAX_LEN)
                throw new ValidationException(message: $"User comment is longer than {COMMENT_MAX_LEN} characters.");
        }
    }
}