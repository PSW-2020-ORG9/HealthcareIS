using Feedback.API.Model.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Services
{
    public interface IUserFeedbackService
    {
        IEnumerable<UserFeedback> GetAll();
        UserFeedback Create(UserFeedback userFeedback);
        UserFeedback Update(UserFeedback userFeedback);
        void Publish(int id);
        IEnumerable<UserFeedback> GetAllPublished();
    }
}
