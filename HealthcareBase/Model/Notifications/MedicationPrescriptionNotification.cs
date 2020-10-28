// File:    MedicationPrescriptionNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:46:49
// Purpose: Definition of Class MedicationPrescriptionNotification

using Model.Medication;
using System;

namespace Model.Notifications
{
    public class MedicationPrescriptionNotification : Notification
    {
        private Model.Medication.MedicationPrescription prescription;

        public MedicationPrescription Prescription { get => prescription; set => prescription = value; }
    }
}