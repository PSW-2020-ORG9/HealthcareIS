// File:    DiagnosisService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class DiagnosisService

using Model.Miscellaneous;
using Repository.MiscellaneousRepository;
using System;
using System.Collections.Generic;

namespace Service.MiscellaneousService
{
    public class DiagnosisService
    {
        private DiagnosisRepository diagnosisRepository;

        public DiagnosisService(DiagnosisRepository diagnosisRepository)
        {
            this.diagnosisRepository = diagnosisRepository;
        }

        public Diagnosis GetByID(String id)
        {
            return diagnosisRepository.GetByID(id);
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            return diagnosisRepository.GetAll();
        }

        public IEnumerable<Diagnosis> Search(String keyword)
        {
            if (keyword is null || keyword.Equals(""))
                return diagnosisRepository.GetAll();
            else
                return diagnosisRepository.GetByKeyword(keyword);
        }

    }
}