// File:    DiagnosisService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class DiagnosisService

using System.Collections.Generic;
using Model.Miscellaneous;
using Repository.MiscellaneousRepository;

namespace Service.MiscellaneousService
{
    public class DiagnosisService
    {
        private readonly DiagnosisRepository diagnosisRepository;

        public DiagnosisService(DiagnosisRepository diagnosisRepository)
        {
            this.diagnosisRepository = diagnosisRepository;
        }

        public Diagnosis GetByID(string id)
        {
            return diagnosisRepository.GetByID(id);
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            return diagnosisRepository.GetAll();
        }

        public IEnumerable<Diagnosis> Search(string keyword)
        {
            if (keyword is null || keyword.Equals(""))
                return diagnosisRepository.GetAll();
            return diagnosisRepository.GetByKeyword(keyword);
        }
    }
}