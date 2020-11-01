// File:    Repository.cs
// Author:  Lana
// Created: 02 May 2020 14:46:23
// Purpose: Definition of Interface Repository

using System;
using System.Collections.Generic;

namespace Repository.Generics
{
    public interface Repository<T, ID> : IWrappableRepository where T : Entity<ID>
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