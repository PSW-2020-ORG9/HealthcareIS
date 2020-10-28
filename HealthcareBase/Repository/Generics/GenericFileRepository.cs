// File:    GenericFileRepository.cs
// Author:  Lana
// Created: 20 May 2020 17:12:57
// Purpose: Definition of Class GenericFileRepository

using Model.CustomExceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository.Generics
{
    // TODO: Full size refactor necessary for this class
    // Migrate from File reading to SQL
    public abstract class GenericFileRepository<T, ID> : Repository<T, ID> where T : Entity<ID>
     where ID : IComparable
    {
        protected string filePath;

        protected GenericFileRepository(string filePath)
        {
            this.filePath = filePath;
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }                
        }

        protected abstract ID GenerateKey(T entity);

        protected abstract T ParseEntity(T entity);

        protected List<T> ReadFile()
        {
            // TODO 
            return new List<T>();
        }

        protected void WriteFile(List<T> entityList)
        {
            // TODO 
        }

        protected IEnumerable<ID> GetAllKeys()
        {
            IEnumerable<T> entityList = ReadFile();
            return entityList.Select(entity => entity.GetKey());
        }

        public virtual int Count()
        {
            IEnumerable<T> entityList = ReadFile();
            return entityList.Count();
        }

        public virtual void Delete(T entity)
        {
            DeleteByID(entity.GetKey());
        }

        public virtual void DeleteByID(ID id)
        {
            List<T> entityList = ReadFile();

            T entity = FindByID(id, entityList);
            entityList.Remove(entity);

            WriteFile(entityList);
        }

        protected virtual T FindByID(ID id, List<T> entityList)
        {
            foreach (T entity in entityList)
            {
                if (id.Equals(entity.GetKey()))
                    return entity;
            }

            throw new BadRequestException();
        }

        public virtual Boolean ExistsByID(ID id)
        {
            IEnumerable<T> entityList = ReadFile();

            foreach (T entity in entityList)
            {
                if (id.Equals(entity.GetKey()))
                    return true;
            }

            return false;
        }

        public virtual IEnumerable<T> GetMatching(Predicate<T> condition)
        {
            List<T> parsedList = new List<T>();
            List<T> entityList = ReadFile();
            T ent;
            foreach (T entity in entityList)
            {
                ent = ParseEntity(entity);
                if (condition(ent))
                {
                    parsedList.Add(ent);
                }
            }
            return parsedList;
        }

        public virtual IEnumerable<T> GetAll()
        {
            List<T> parsedList = new List<T>();
            List<T> entityList = ReadFile();
            foreach (T entity in entityList)
            {
                parsedList.Add(ParseEntity(entity));
            }
            return parsedList;
        }

        public virtual T GetByID(ID id)
        {
            var entityList = ReadFile();

            foreach (T entity in entityList)
            {
                if (id.Equals(entity.GetKey()))
                    return ParseEntity(entity);
            }

            throw new BadRequestException();
        }

        public virtual T Create(T entity)
        {
            List<T> entityList = ReadFile();

            entity.SetKey(GenerateKey(entity));

            if (!ExistsByID(entity.GetKey()))
                entityList.Add(entity);

            WriteFile(entityList);
            return entity;
        }

        public virtual T Update(T entity)
        {
            List<T> entityList = ReadFile();
            T oldEntity = FindByID(entity.GetKey(), entityList);
            entityList.Remove(oldEntity);

            entityList.Add(entity);

            WriteFile(entityList);
            return entity;
        }

    }
}