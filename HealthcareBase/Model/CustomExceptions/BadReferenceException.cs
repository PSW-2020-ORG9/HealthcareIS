namespace HealthcareBase.Model.CustomExceptions
{
    internal class BadReferenceException : ValidationException
    {
        public BadReferenceException()
        {
        }

        public BadReferenceException(string message) : base(message)
        {
        }
    }
}