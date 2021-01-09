// File:    Surgery.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Surgery

using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Miscellaneous;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class Surgery : Procedure
    {
        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public string CauseOfSurgery { get; set; }
    }
}