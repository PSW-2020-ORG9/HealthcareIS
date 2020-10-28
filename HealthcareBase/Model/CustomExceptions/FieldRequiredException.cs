namespace Model.CustomExceptions
{
    internal class FieldRequiredException : ValidationException
    {
        public FieldRequiredException()
        {
        }

        public FieldRequiredException(string message) : base(message)
        {
        }
    }
}