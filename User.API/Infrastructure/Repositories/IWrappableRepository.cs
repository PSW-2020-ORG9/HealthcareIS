using System;

namespace User.API.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface which offers basic CRUD method implementations, along with a Prepare() method
    /// from the IPreparable interface. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    public interface IWrappableRepository<T, ID> 
        : IPreparable,
        IRepository<T, ID>
        where T : Entity<ID>
        where ID : IComparable
    {
        
    }
}