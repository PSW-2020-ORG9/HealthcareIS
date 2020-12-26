using Feedback.API.Infrastructure;

namespace Feedback.API.Model.User
{
    public class Doctor : Entity<string>
    {
        public Person Person { get; set; }
    }
}
