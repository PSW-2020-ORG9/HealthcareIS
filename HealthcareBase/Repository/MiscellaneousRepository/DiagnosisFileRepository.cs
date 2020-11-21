// File:    DiagnosisFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class DiagnosisFileRepository

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.MiscellaneousRepository
{
    public class DiagnosisFileRepository : GenericFileRepository<Diagnosis, string>, DiagnosisRepository
    {
        public DiagnosisFileRepository(string filePath) : base(filePath)
        {
        }

        public IEnumerable<Diagnosis> GetByKeyword(string keyword)
        {
            return GetMatching(diagnosis => diagnosis.Name.Contains(keyword));
        }

        protected override string GenerateKey(Diagnosis entity)
        {
            if (entity.Icd is null || entity.Icd.Equals(""))
                throw new BadRequestException();
            return entity.Icd;
        }

        protected override Diagnosis ParseEntity(Diagnosis entity)
        {
            return entity;
        }
    }
}