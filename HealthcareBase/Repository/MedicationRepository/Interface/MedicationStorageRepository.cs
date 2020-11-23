// File:    MedicationStorageRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:05
// Purpose: Definition of Interface MedicationStorageRepository

using Model.Medication;
using Model.StorageRecords;
using Repository.Generics;

namespace Repository.MedicationRepository
{
    public interface MedicationStorageRepository : IWrappableRepository<MedicationStorageRecord, int>
    {
        MedicationStorageRecord GetByMedication(Medication medication);
    }
}