using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.Generics.Interface
{
    public interface IRepository<T, ID>
        where T : IEntity<ID>
        where ID : IComparable
    {
        int Count();
        void Delete(T entity);
        void DeleteByID(ID id);
        bool ExistsByID(ID id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMatching(Expression<Func<T, bool>> condition);
        IEnumerable<T> GetMatching(IEnumerable<Expression<Func<T, bool>>> conditions);
        int CountMatching(Expression<Func<T, bool>> condition);
        int CountMatching(IEnumerable<Expression<Func<T, bool>>> conditions);
        T GetByID(ID id);
        T Create(T entity);
        T Update(T entity);
    }
}