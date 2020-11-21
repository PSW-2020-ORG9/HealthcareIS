// File:    MedicationStorageRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:05
// Purpose: Definition of Interface MedicationStorageRepository

using HealthcareBase.Model.Medication;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.MedicationRepository
{
    public interface MedicationStorageRepository : IWrappableRepository<MedicationStorageRecord, int>
    {
        MedicationStorageRecord GetByMedication(Medication medication);
    }
}