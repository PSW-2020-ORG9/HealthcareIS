// File:    DiagnosisRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DiagnosisRepository

using Model.Miscellaneous;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.MiscellaneousRepository
{
    public interface DiagnosisRepository : Repository<Diagnosis, String>
    {
        IEnumerable<Diagnosis> GetByKeyword(String keyword);

    }
}