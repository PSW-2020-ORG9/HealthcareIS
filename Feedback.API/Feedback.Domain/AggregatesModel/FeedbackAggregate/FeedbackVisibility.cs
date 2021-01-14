using Feedback.API.Feedback.Domain.Exceptions;

namespace Feedback.API.Feeback.Domain.AggregatesModel.FeedbackAggregate
{
    public class FeedbackVisibility
    {
        public bool IsPublic { get; private set; }
        public bool IsAnonymous { get; private set; }
        public bool IsPublished { get; private set; }

        public FeedbackVisibility() {}

        public FeedbackVisibility(bool isPublic, bool isAnonymous, bool isPublished)
        {
            Validate(isPublic, isPublished);
            IsPublic = isPublic;
            IsAnonymous = isAnonymous;
            IsPublished = isPublished;
        }

        private void Validate(bool isPublic, bool isPublished)
        {
            if (isPublished && !isPublic)
                throw new ValidationException("Feedback cannot be published when its visibility is restricted.");
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