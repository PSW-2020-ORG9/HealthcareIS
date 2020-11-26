// File:    MedicationPrescription.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class MedicationPrescription
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Medication
{
    public class MedicationPrescription : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public DateTime Date { get; set; }
        
        [ForeignKey("ExaminationReport")]
        public int ExaminationReportId { get; set; }
        public ExaminationReport ExaminationReport { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [ForeignKey("Medication")]
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        
        [ForeignKey("Instructions")]
        public int InstructionsId { get; set; }
        public IntakeInstructions Instructions { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}