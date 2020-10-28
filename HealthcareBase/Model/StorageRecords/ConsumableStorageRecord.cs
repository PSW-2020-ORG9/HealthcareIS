// File:    ConsumableStorageRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:19:45
// Purpose: Definition of Class ConsumableStorageRecord

using Model.HospitalResources;

namespace Model.StorageRecords
{
    public class ConsumableStorageRecord : StorageRecord
    {
        public MedicalConsumable Consumable { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ConsumableStorageRecord record &&
                   id == record.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}