namespace HealthcareBase.Model.CustomExceptions
{
    internal class NotUniqueException : ValidationException
    {
        public NotUniqueException()
        {
        }

        public NotUniqueException(string message) : base(message)
        {
        }
    }
}