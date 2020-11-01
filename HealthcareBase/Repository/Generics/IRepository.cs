using System;
using System.Collections.Generic;

namespace Repository.Generics
{
    public interface IRepository<T, ID>
        where T : Entity<ID>
        where ID : IComparable
    {
        int Count();
        void Delete(T entity);
        void DeleteByID(ID id);
        bool ExistsByID(ID id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMatching(Predicate<T> condition);
        T GetByID(ID id);
        T Create(T entity);
        T Update(T entity);
    }
}