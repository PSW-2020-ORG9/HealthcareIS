// File:    ConsumableStorageRecordRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface ConsumableStorageRecordRepository

using Model.HospitalResources;
using Model.StorageRecords;
using Repository.Generics;
using System;

namespace Repository.HospitalResourcesRepository
{
    public interface ConsumableStorageRecordRepository : Repository<ConsumableStorageRecord, int>
    {
        ConsumableStorageRecord GetByMedicalConsumable(MedicalConsumable medicalConsumable);
    }
}