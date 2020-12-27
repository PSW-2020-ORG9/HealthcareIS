using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Schedule.API.Infrastructure.Database;
using User.API.Infrastructure.Repositories;

namespace Schedule.API.Infrastructure.Repositories
{
    public class GenericSqlRepository<T, ID> 
        : IWrappableRepository<T, ID> 
        where T : Entity<ID>
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
            entity.Id = id;
            Query().Remove(entity);
            SaveChanges();
        }
 
        public int Count()
            => Query().Count();
 
        public bool ExistsByID(ID id) 
            => GetByID(id) != default;
 
        public T GetByID(ID id)
            => GetMatching(entity => entity.Id.Equals(id)).FirstOrDefault();
 
        public IEnumerable<T> GetMatching(Expression<Func<T, bool>> condition)
            => IncludeFields(Query()).Where(condition).ToList();

        public IEnumerable<T> GetMatching(IEnumerable<Expression<Func<T, bool>>> conditions)
        {
            IQueryable<T> query = IncludeFields(Query());
            conditions.ToList().ForEach(condition => query = query.Where(condition));
            return query.ToList();
        }

        public IEnumerable<Dto> GetColumnsForMatching<Dto>(
            Expression<Func<T, bool>> condition,
            Expression<Func<T, Dto>> selection
        )
            => IncludeFields(Query()).Where(condition).Select(selection).ToList();
        
        public int CountMatching(Expression<Func<T, bool>> condition)
            => IncludeFields(Query()).Where(condition).Count();

        public int CountMatching(IEnumerable<Expression<Func<T, bool>>> conditions)
        {
            IQueryable<T> query = IncludeFields(Query());
            conditions.ToList().ForEach(condition => query = query.Where(condition));
            return query.Count();
        }

        public IEnumerable<T> GetAll() 
            => IncludeFields(Query()).ToList();
 
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