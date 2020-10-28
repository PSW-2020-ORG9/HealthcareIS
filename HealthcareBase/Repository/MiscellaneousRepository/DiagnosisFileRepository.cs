// File:    DiagnosisFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class DiagnosisFileRepository

using Model.CustomExceptions;
using Model.Miscellaneous;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.MiscellaneousRepository
{
    public class DiagnosisFileRepository : GenericFileRepository<Diagnosis, String>, DiagnosisRepository
    {
        public DiagnosisFileRepository(String filePath) : base(filePath)
        {

        }

        public IEnumerable<Diagnosis> GetByKeyword(String keyword)
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