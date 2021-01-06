using Feedback.API.Infrastructure;
using Feedback.API.Model.User;
using General;
using System;

namespace Feedback.API.Model.Feedback
{
    public class UserFeedback : Entity<int>
    {
        public DateTime Date { get; set; }
        public string UserComment { get; set; }
        public FeedbackVisibility FeedbackVisibility { get; set; }
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }
    }
}