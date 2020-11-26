// File:    MedicationStorageRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:22:13
// Purpose: Definition of Class MedicationStorageRecord

using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareBase.Model.StorageRecords
{
    public class MedicationStorageRecord : StorageRecord
    {
        [ForeignKey("Medication")]
        public int MedicationId { get; set; }
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