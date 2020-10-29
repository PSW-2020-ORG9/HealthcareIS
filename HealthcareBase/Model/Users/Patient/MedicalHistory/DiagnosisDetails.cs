// File:    DiagnosisDetails.cs
// Author:  Gudli
// Created: 21 April 2020 15:39:06
// Purpose: Definition of Class DiagnosisDetails

using Microsoft.EntityFrameworkCore;
using Model.Miscellaneous;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class DiagnosisDetails
    {
        public int DiscoveredAtAge { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public ConditionType Type { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}