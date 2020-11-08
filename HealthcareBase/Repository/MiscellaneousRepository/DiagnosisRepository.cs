// File:    DiagnosisRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DiagnosisRepository

using System.Collections.Generic;
using Model.Miscellaneous;
using Repository.Generics;

namespace Repository.MiscellaneousRepository
{
    public interface DiagnosisRepository : IWrappableRepository<Diagnosis, string>
    {
        IEnumerable<Diagnosis> GetByKeyword(string keyword);
    }
}