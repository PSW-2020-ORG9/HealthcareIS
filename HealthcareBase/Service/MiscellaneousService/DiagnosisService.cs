// File:    DiagnosisService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class DiagnosisService

using System.Collections.Generic;
using Model.Miscellaneous;
using Repository.Generics;
using Repository.MiscellaneousRepository;

namespace Service.MiscellaneousService
{
    public class DiagnosisService
    {
        private readonly RepositoryWrapper<DiagnosisRepository> diagnosisRepository;

        public DiagnosisService(RepositoryWrapper<DiagnosisRepository> diagnosisRepository)
        {
            this.diagnosisRepository = diagnosisRepository;
        }

        public Diagnosis GetByID(string id)
        {
            return diagnosisRepository.Repository.GetByID(id);
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            return diagnosisRepository.Repository.GetAll();
        }

        public IEnumerable<Diagnosis> Search(string keyword)
        {
            if (keyword is null || keyword.Equals(""))
                return diagnosisRepository.Repository.GetAll();
            return diagnosisRepository.Repository.GetByKeyword(keyword);
        }
    }
}