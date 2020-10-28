// File:    MedicationRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:04:39
// Purpose: Definition of Interface MedicationRepository

using Model.Medication;
using Repository.Generics;

namespace Repository.MedicationRepository
{
    public interface MedicationRepository : Repository<Medication, int>
    {
    }
}