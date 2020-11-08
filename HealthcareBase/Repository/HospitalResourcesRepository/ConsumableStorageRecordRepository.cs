// File:    ConsumableStorageRecordRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface ConsumableStorageRecordRepository

using Model.HospitalResources;
using Model.StorageRecords;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public interface ConsumableStorageRecordRepository : IWrappableRepository<ConsumableStorageRecord, int>
    {
        ConsumableStorageRecord GetByMedicalConsumable(MedicalConsumable medicalConsumable);
    }
}