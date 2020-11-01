namespace Repository.Generics
{
    public interface IWrappableRepository
    {
        /// <summary>
        /// Calls any necessary actions on the repository before the actual data transfer occurs. 
        /// </summary>
        public void PrepareTransaction();
    }
}