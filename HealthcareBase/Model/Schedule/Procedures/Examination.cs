// File:    Examination.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Examination

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Miscellaneous;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class Examination : Procedure
    {
        private List<MedicationPrescription> prescriptions;

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public string Anamnesis { get; set; }

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
                    foreach (var oMedicationPrescription in value)
                        AddPrescriptions(oMedicationPrescription);
            }
        }

        public void AddPrescriptions(MedicationPrescription newMedicationPrescription)
        {
            if (newMedicationPrescription == null)
                return;
            if (prescriptions == null)
                prescriptions = new List<MedicationPrescription>();
            if (!prescriptions.Contains(newMedicationPrescription))
                prescriptions.Add(newMedicationPrescription);
        }

        public void RemovePrescriptions(MedicationPrescription oldMedicationPrescription)
        {
            if (oldMedicationPrescription == null)
                return;
            if (prescriptions != null)
                if (prescriptions.Contains(oldMedicationPrescription))
                    prescriptions.Remove(oldMedicationPrescription);
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