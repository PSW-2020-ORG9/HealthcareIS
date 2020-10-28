// File:    MedicationStorageRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:22:13
// Purpose: Definition of Class MedicationStorageRecord

namespace Model.StorageRecords
{
    public class MedicationStorageRecord : StorageRecord
    {
        public Medication.Medication Medication { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MedicationStorageRecord record &&
                   id == record.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}