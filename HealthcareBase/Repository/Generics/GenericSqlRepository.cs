using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Repository.Generics
{
    public class GenericSqlRepository<T, ID> 
        : IWrappableRepository<T, ID> 
        where T : class, Entity<ID>
        where ID : IComparable
    {
        private readonly IContextFactory _contextFactory;
        private DbContext _context;
        
        public GenericSqlRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Prepare()
        {
            _context = _contextFactory.CreateContext();
        }

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

        public T GetByID(ID id)
            => Query().FirstOrDefault(entity => entity.GetKey().Equals(id));

        public IEnumerable<T> GetMatching(Predicate<T> condition) 
            => Query().Where(entity => condition(entity)).ToList();

        public IEnumerable<T> GetAll() 
            => GetAllIncluded().ToList();
        
        private IEnumerable<T> GetAllIncluded()
        {
            IQueryable<T> entities = Query();
            GetModelProperties().ToList().ForEach(property =>
            {
                // Performs a Join operation on the given Property
                // TODO Check whether this fetches multiple depths of references
                entities = entities.Include(property.Name);
            });
            return entities;
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
        

        /// <summary>
        /// Returns a list of properties for the object-type tied to this repository instance.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PropertyInfo> GetModelProperties()
        {
            return _context.Model.GetEntityTypes(typeof(T)).FirstOrDefault()!
                .GetNavigations().Select(p => p.PropertyInfo);
        }
    }
}
