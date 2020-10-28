// File:    Examination.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Examination

using Model.Medication;
using Model.Miscellaneous;
using System;
using System.Collections.Generic;

namespace Model.Schedule.Procedures
{
    public class Examination : Procedure
    {
        private Diagnosis diagnosis;
        private String anamnesis;
        private List<MedicationPrescription> prescriptions;

        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
        public string Anamnesis { get => anamnesis; set => anamnesis = value; }

        public IEnumerable<MedicationPrescription> Prescriptions
        {
            get
            {
                if (prescriptions == null)
                    prescriptions = new List<MedicationPrescription>();
                return prescriptions;
            }
            set
            {
                RemoveAllPrescriptions();
                if (value != null)
                {
                    foreach (Model.Medication.MedicationPrescription oMedicationPrescription in value)
                        AddPrescriptions(oMedicationPrescription);
                }
            }
        }

        public void AddPrescriptions(Model.Medication.MedicationPrescription newMedicationPrescription)
        {
            if (newMedicationPrescription == null)
                return;
            if (this.prescriptions == null)
                this.prescriptions = new List<MedicationPrescription>();
            if (!this.prescriptions.Contains(newMedicationPrescription))
                this.prescriptions.Add(newMedicationPrescription);
        }

        public void RemovePrescriptions(Model.Medication.MedicationPrescription oldMedicationPrescription)
        {
            if (oldMedicationPrescription == null)
                return;
            if (this.prescriptions != null)
                if (this.prescriptions.Contains(oldMedicationPrescription))
                    this.prescriptions.Remove(oldMedicationPrescription);
        }

        public void RemoveAllPrescriptions()
        {
            if (prescriptions != null)
                prescriptions.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is Examination procedure &&
                   id == procedure.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}