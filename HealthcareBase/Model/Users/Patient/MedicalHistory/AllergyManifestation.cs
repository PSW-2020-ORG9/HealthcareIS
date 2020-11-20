// File:    AllergyManifestation.cs
// Author:  Gudli
// Created: 20 April 2020 17:03:34
// Purpose: Definition of Class AllergyManifestation

using Microsoft.EntityFrameworkCore;
using Model.Miscellaneous;
using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    public class AllergyManifestation : Entity<int>
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public AllergyIntensity Intensity { get; set; }

        [ForeignKey("Allergy")]
        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

        [ForeignKey("MedicalHistory")]
        public int MedicalHistoryId { get; set; }
        public MedicalHistory MedicalHistory { get; set; }
        public int GetKey()
            => Id;

        public void SetKey(int id)
            => Id = id;
    }
}