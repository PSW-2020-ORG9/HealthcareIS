// File:    Surgery.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Surgery

using Model.Miscellaneous;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Schedule.Procedures
{
    public class Surgery : Procedure
    {
        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public string CauseOfSurgery { get; set; }
    }
}