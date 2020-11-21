// File:    ConsumableStorageRecordRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface ConsumableStorageRecordRepository

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface ConsumableStorageRecordRepository : IWrappableRepository<ConsumableStorageRecord, int>
    {
        ConsumableStorageRecord GetByMedicalConsumable(MedicalConsumable medicalConsumable);
    }
}