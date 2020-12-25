namespace Feedback.API.Infrastructure.Repositories
{
    public interface IPreparable
    {
        /// <summary>
        /// Calls any necessary actions on the repository before the actual data transfer occurs. 
        /// </summary>
        public void Prepare();
    }
}