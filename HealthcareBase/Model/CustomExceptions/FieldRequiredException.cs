namespace HealthcareBase.Model.CustomExceptions
{
    public class FieldRequiredException : ValidationException
    {
        public FieldRequiredException()
        {
        }

        public FieldRequiredException(string message) : base(message)
        {
        }
    }
}