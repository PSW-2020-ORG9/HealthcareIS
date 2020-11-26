// File:    FamilyMember.cs
// Author:  Gudli
// Created: 21 April 2020 15:23:25
// Purpose: Definition of Class FamilyMember

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Patient.MedicalHistory
{
    public class FamilyMemberDiagnosis : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string FamilyRelation { get; set; }
        public int DiscoveredAtAge { get; set; }
        public bool Lethal { get; set; }
        public string Description { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}