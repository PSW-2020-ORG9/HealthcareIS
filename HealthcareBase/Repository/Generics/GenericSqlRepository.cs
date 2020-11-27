using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HealthcareBase.Model.Database;
using HealthcareBase.Repository.Generics.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.Generics
{
    public class GenericSqlRepository<T, ID> 
        : IWrappableRepository<T, ID> 
        where T : class, IEntity<ID>
        where ID : IComparable
    {
        private readonly IContextFactory _contextFactory;
        private DbContext _context;
 
        public GenericSqlRepository(IContextFactory contextFactory) 
            => _contextFactory = contextFactory;
 
        public void Prepare()
            => _context = _contextFactory.CreateContext();
 
        public T Create(T entity)
        {
            var createdEntity = Query().Add(entity).Entity;
            SaveChanges();
            return createdEntity;
        }
 
        public T Update(T entity)
        {
            var updatedEntity = Query().Update(entity).Entity;
            SaveChanges();
            return updatedEntity;
        }
 
        public void Delete(T entity)
        {
            Query().Remove(entity);
            SaveChanges();
        }
 
        public void DeleteByID(ID id)
        {
            var entity = Activator.CreateInstance<T>();
            entity.SetKey(id);
            Query().Remove(entity);
            SaveChanges();
        }
 
        public int Count()
            => Query().Count();
 
        public bool ExistsByID(ID id) 
            => GetByID(id) != default;
 
        // TODO GetKey() and other methods CANNOT be converted to an SQL query. Properties are required.
        public T GetByID(ID id)
            => GetAll().FirstOrDefault(entity => entity.GetKey().Equals(id));
 
        public IEnumerable<T> GetMatching(Expression<Func<T, bool>> condition)
            => IncludeFields(Query()).Where(condition);

        public IEnumerable<T> GetMatching(IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            IQueryable<T> query = IncludeFields(Query());
            expressions.ToList().ForEach(expression => query = query.Where(expression));
            return query;
        }

        public int CountMatching(Expression<Func<T, bool>> condition)
            => IncludeFields(Query()).Where(condition).Count();
        
        public IEnumerable<T> GetAll() 
            => IncludeFields(Query());
 
        /// <summary>
        /// Method which allows extending Repository classes to include custom fields
        /// for their respective classes.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> IncludeFields(IQueryable<T> query)
        {
            return query;
        }
 
        /// <summary>
        /// Returns the default DbSet for the current Repository instance.
        /// </summary>
        /// <returns></returns>
        private DbSet<T> Query()
            => _context.Set<T>();
 
 
        /// <summary>
        /// Saves all currently pending changes to the database.
        /// </summary>
        private void SaveChanges() 
            => _context.SaveChanges();
    }
}