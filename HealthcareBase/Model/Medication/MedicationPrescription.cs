// File:    MedicationPrescription.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class MedicationPrescription

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Miscellaneous;
using Model.Users.Employee;
using Model.Users.Patient;
using Repository.Generics;

namespace Model.Medication
{
    public class MedicationPrescription : Entity<int>
    {
        public DateTime Date { get; set; }

        [ForeignKey("Diagnosis")]
        public string? DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [ForeignKey("Medication")]
        public int? MedicationId { get; set; }
        public Medication Medication { get; set; }
        public IntakeInstructions Instructions { get; set; }

        [ForeignKey("PrescribedBy")]
        public int PrescribedById { get; set; }
        public Doctor PrescribedBy { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Key]
        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is MedicationPrescription prescription &&
                   Id == prescription.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}