// File:    DiagnosisDetails.cs
// Author:  Gudli
// Created: 21 April 2020 15:39:06
// Purpose: Definition of Class DiagnosisDetails

using Microsoft.EntityFrameworkCore;
using Model.Miscellaneous;
using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    public class DiagnosisDetails : Entity<int>
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public ConditionType Type { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}