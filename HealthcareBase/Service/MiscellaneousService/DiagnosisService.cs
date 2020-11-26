// File:    DiagnosisService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class DiagnosisService

using System.Collections.Generic;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MiscellaneousRepository;

namespace HealthcareBase.Service.MiscellaneousService
{
    public class DiagnosisService
    {
        private readonly RepositoryWrapper<IDiagnosisRepository> diagnosisRepository;

        public DiagnosisService(IDiagnosisRepository diagnosisRepository)
        {
            this.diagnosisRepository = new RepositoryWrapper<IDiagnosisRepository>(diagnosisRepository);
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