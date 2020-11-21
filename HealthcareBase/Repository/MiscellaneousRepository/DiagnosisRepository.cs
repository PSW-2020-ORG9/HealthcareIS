// File:    DiagnosisRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DiagnosisRepository

using System.Collections.Generic;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.MiscellaneousRepository
{
    public interface DiagnosisRepository : IWrappableRepository<Diagnosis, string>
    {
        IEnumerable<Diagnosis> GetByKeyword(string keyword);
    }
}