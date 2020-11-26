// File:    MedicationStorageRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:05
// Purpose: Definition of Interface MedicationStorageRepository

using HealthcareBase.Model.Medication;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.MedicationRepository.Interface
{
    public interface IMedicationStorageRepository : IWrappableRepository<MedicationStorageRecord, int>
    {
        MedicationStorageRecord GetByMedication(Medication medication);
    }
}