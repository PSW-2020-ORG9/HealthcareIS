// File:    FamilyMember.cs
// Author:  Gudli
// Created: 21 April 2020 15:23:25
// Purpose: Definition of Class FamilyMember

using Microsoft.EntityFrameworkCore;
using Model.Miscellaneous;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class FamilyMemberDiagnosis
    {
        public string FamilyRelation { get; set; }

        public int DiscoveredAtAge { get; set; }

        public bool Lethal { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
    }
}