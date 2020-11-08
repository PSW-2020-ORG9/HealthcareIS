// File:    MedicationPrescriptionNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:46:49
// Purpose: Definition of Class MedicationPrescriptionNotification

using Model.Medication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Notifications
{
    public class MedicationPrescriptionNotification : Notification
    {
        [ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public MedicationPrescription Prescription { get; set; }
    }
}