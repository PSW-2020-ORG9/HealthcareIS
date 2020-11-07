using System;
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
        public static void validate(UserFeedback entity)
        {
            CheckForNullValues(entity);
            CheckFields(entity);
        }
        /// <summary>
        /// Checks if passed object or any of its fields are null values.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FieldRequiredException"></exception>
        private static void CheckForNullValues(UserFeedback entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            if (entity.Date==null) 
                throw new FieldRequiredException("Field 'Date' cannot be null.");
            if (entity.UserComment==null)
                throw new FieldRequiredException("Field 'UserComment' cannot be null.");

        }
        /// <summary>
        /// Validates object fields content.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ValidationException"></exception>
        private static void CheckFields(UserFeedback entity)
        {
            IsCommentEmpty(entity.UserComment);
            IsCommentTooLong(entity.UserComment);
            IsDateSetInFuture(entity.Date);
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
                throw new ValidationException("User comment is longer than 300 characters.");
        }
        /// <summary>
        /// Checks if comment date is set in the future.
        /// </summary>
        /// <param name="date"></param>
        /// <exception cref="ValidationException"></exception>
        private static void IsDateSetInFuture(DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
                throw new ValidationException("Comment date cannot be set in the future.");
        }
    }
}