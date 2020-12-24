using HealthcareBase.Model.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Users.UserFeedback
{
    [Owned]
    public class FeedbackVisibility
    {
        public bool IsPublic { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsPublished { get; set; }

        public FeedbackVisibility() {}

        private FeedbackVisibility(bool isPublic, bool isAnonymous, bool isPublished)
        {
            IsPublic = isPublic;
            IsAnonymous = isAnonymous;
            IsPublished = isPublished;
        }
        public FeedbackVisibility Publish()
        {
            ValidateForPublishing();
            return new FeedbackVisibility(IsPublic,IsAnonymous,true);
        }

        private void ValidateForPublishing()
        {
            if (IsPublished) throw new ValidationException("Feedback is already published.");
            if (!IsPublic) throw new ValidationException("Feedback visibility is restricted.");
        }
    }
}