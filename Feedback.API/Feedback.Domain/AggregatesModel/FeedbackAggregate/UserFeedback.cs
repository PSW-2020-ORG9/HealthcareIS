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

        protected UserFeedback() { }

        public UserFeedback(DateTime date, string userComment, FeedbackVisibility feedbackVisibility, int patientAccountId) {

            ValidateCommentNotEmpty(userComment);
            ValidateCommentLength(userComment);
            Date = date;
            UserComment = userComment;
            FeedbackVisibility = feedbackVisibility;
            PatientAccountId = patientAccountId;
        }

        public void PublishFeedback()
        {
            FeedbackVisibility.Publish();
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