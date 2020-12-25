using Feedback.API.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback.API.Model.Feedback
{
    public class UserFeedback : Entity<int>
    {
        public DateTime Date { get; set; }
        public string UserComment { get; set; }
        public FeedbackVisibility FeedbackVisibility { get; set; }
        [ForeignKey("PatientAccount")]
        public int PatientAccountId { get; set; }
        //public PatientAccount PatientAccount { get; set; }
    }
}