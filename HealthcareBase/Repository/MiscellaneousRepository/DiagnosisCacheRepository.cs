// File:    DiagnosisCacheRepository.cs
// Author:  Lana
// Created: 02 June 2020 00:41:57
// Purpose: Definition of Class DiagnosisCacheRepository

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model.Miscellaneous;

namespace Repository.MiscellaneousRepository
{
    public class DiagnosisCacheRepository : DiagnosisRepository
    {
        private readonly DiagnosisFileRepository diagnosisFileRepository;

        public DiagnosisCacheRepository(DiagnosisFileRepository diagnosisFileRepository)
        {
            this.diagnosisFileRepository = diagnosisFileRepository;
        }

        public int Count()
        {
            return ((DiagnosisRepository) diagnosisFileRepository).Count();
        }

        public Diagnosis Create(Diagnosis entity)
        {
            return ((DiagnosisRepository) diagnosisFileRepository).Create(entity);
        }

        public void Delete(Diagnosis entity)
        {
            ((DiagnosisRepository) diagnosisFileRepository).Delete(entity);
        }

        public void DeleteByID(string id)
        {
            ((DiagnosisRepository) diagnosisFileRepository).DeleteByID(id);
        }

        public bool ExistsByID(string id)
        {
            return ((DiagnosisRepository) diagnosisFileRepository).ExistsByID(id);
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            return ((DiagnosisRepository) diagnosisFileRepository).GetAll();
        }

        public Diagnosis GetByID(string id)
        {
            return ((DiagnosisRepository) diagnosisFileRepository).GetByID(id);
        }

        public IEnumerable<Diagnosis> GetMatching(Expression<Func<Diagnosis, bool>> condition)
        {
            return ((DiagnosisRepository) diagnosisFileRepository).GetMatching(condition);
        }

        public IEnumerable<Diagnosis> GetByKeyword(string keyword)
        {
            return ((DiagnosisRepository) diagnosisFileRepository).GetByKeyword(keyword);
        }

        public Diagnosis Update(Diagnosis entity)
        {
            return ((DiagnosisRepository) diagnosisFileRepository).Update(entity);
        }

        public void Prepare() {  }
    }
}