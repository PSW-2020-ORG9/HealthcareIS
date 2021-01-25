using Feedback.API.DTOs;
using General;
using System;

namespace Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate
{
    public class UserFeedback : Entity<int>
    {
        public DateTime Date { get; set; }
        public string UserComment { get; set; }
        public FeedbackVisibility FeedbackVisibility { get; set; }
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }

        public UserFeedback() { }

        public UserFeedback(UserFeedbackDTO dto)
        {
            ValidateCommentNotEmpty(dto.UserComment);
            ValidateCommentLength(dto.UserComment);
            FeedbackVisibility fv = new FeedbackVisibility(dto.IsPublic, dto.IsAnonymous, dto.IsPublished);
            Date = DateTime.Now;
            UserComment = dto.UserComment;
            FeedbackVisibility = fv;
            PatientAccountId = dto.UserId;
        }

        public void PublishFeedback()
        {
           FeedbackVisibility =  FeedbackVisibility.Publish();
        }

        private static void ValidateCommentNotEmpty(string comment)
        {
            if (comment.Trim().Equals(""))
                throw new ArgumentException("User comment cannot be empty.");
        }

        private static void ValidateCommentLength(string comment)
        {
            const int COMMENT_MAX_LEN = 300;
            if (comment.Length > COMMENT_MAX_LEN)
                throw new ArgumentException(message: $"User comment is longer than {COMMENT_MAX_LEN} characters.");
        }
    }
}