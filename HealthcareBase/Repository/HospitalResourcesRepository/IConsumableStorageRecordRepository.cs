// File:    ConsumableStorageRecordRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface ConsumableStorageRecordRepository

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface IConsumableStorageRecordRepository : IWrappableRepository<ConsumableStorageRecord, int>
    {
        ConsumableStorageRecord GetByMedicalConsumable(MedicalConsumable medicalConsumable);
    }
}