namespace Model.CustomExceptions
{
    internal class TimingException : ValidationException
    {
        public TimingException()
        {
        }

        public TimingException(string message) : base(message)
        {
        }
    }
}