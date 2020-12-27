// File:    MedicationPrescription.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class MedicationPrescription

using System.ComponentModel.DataAnnotations.Schema;
using General;

namespace Hospital.API.Model.Medication
{
    public class MedicationPrescription : Entity<int>
    {
        public int MedicalRecordId { get; set; }
        public int ExaminationReportId { get; set; }
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [ForeignKey("Medication")]
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        
        [ForeignKey("Instructions")]
        public int InstructionsId { get; set; }
        public IntakeInstructions Instructions { get; set; }
    }
}