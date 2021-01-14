using General;
using System;

namespace Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate
{
    public class UserFeedback : Entity<int>
    {
        private DateTime _date;
        private string _userComment;
        public FeedbackVisibility FeedbackVisibility { get; private set; }
        private int _patientAccountId;

        public PatientAccount PatientAccount { get; set; }

        protected UserFeedback() { }

        public UserFeedback(DateTime date, string userComment, FeedbackVisibility feedbackVisibility, int patientAccountId) {

            ValidateCommentNotEmpty(userComment);
            ValidateCommentLength(userComment);
            _date = date;
            _userComment = userComment;
            FeedbackVisibility = feedbackVisibility;
            _patientAccountId = patientAccountId;
        }

        public DateTime GetDate() { return _date; }
        public string GetUserComment() { return _userComment; }
        public int GetPatientAccountId() { return _patientAccountId; }

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