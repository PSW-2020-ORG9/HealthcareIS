using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repository.Generics
{
    public class GenericSqlRepository<T, ID> 
        : IWrappableRepository,  
        Repository<T, ID> 
        where T : class, Entity<ID>
        where ID : IComparable
    {
        private readonly IContextFactory _contextFactory;
        private DbContext _context;
        
        public GenericSqlRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void PrepareTransaction()
        {
            _context = _contextFactory.CreateContext();
        }

        public int Count()
        {
            var context = _contextFactory.CreateContext();
            return context.Set<T>().Count();
        }

        public T Create(T entity)
        {
            var context = _contextFactory.CreateContext();
            entity = context.Set<T>().Add(entity).Entity;
            context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            var context = _contextFactory.CreateContext();
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public void DeleteByID(ID id)
        {
            var context = _contextFactory.CreateContext();
            T entity = context.Set<T>().Find(id);
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public bool ExistsByID(ID id)
        {
            T entity = GetByID(id);
            return entity != default;
        }

        public IEnumerable<T> GetAll()
            => GetAllEager().ToList();

        public T GetByID(ID id)
            => GetAllEager().ToList().Where(x => id.Equals(x.GetKey())).FirstOrDefault();

        public IEnumerable<T> GetMatching(Predicate<T> condition)
        {
            List<T> entities = new List<T>();
            foreach (T entity in GetAllEager().ToList())
            {
                if (condition(entity)) entities.Add(entity);
            }
            return entities;
        }

        public T Update(T entity)
        {
            var context = _contextFactory.CreateContext();
            entity = context.Set<T>().Update(entity).Entity;
            context.SaveChanges();
            return entity;

        }

        private IQueryable<T> GetAllEager()
        {
            var context = _contextFactory.CreateContext();
            var modelInfo = context.Model.GetEntityTypes(typeof(T)).FirstOrDefault();
            var properties = modelInfo.GetNavigations().Select(p => p.PropertyInfo);
            IQueryable<T> entities = context.Set<T>();
            foreach (var property in properties)
            {
                entities = entities.Include(property.Name);
            }
            return entities;
        }
    }
}
