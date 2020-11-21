// File:    FamilyMember.cs
// Author:  Gudli
// Created: 21 April 2020 15:23:25
// Purpose: Definition of Class FamilyMember

using Microsoft.EntityFrameworkCore;
using Model.Miscellaneous;
using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    public class FamilyMemberDiagnosis : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string FamilyRelation { get; set; }
        public int DiscoveredAtAge { get; set; }
        public bool Lethal { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}