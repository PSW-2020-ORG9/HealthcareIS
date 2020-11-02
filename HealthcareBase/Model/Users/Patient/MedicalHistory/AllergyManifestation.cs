// File:    AllergyManifestation.cs
// Author:  Gudli
// Created: 20 April 2020 17:03:34
// Purpose: Definition of Class AllergyManifestation

using Microsoft.EntityFrameworkCore;
using Model.Miscellaneous;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class AllergyManifestation
    {
        [Column(TypeName = "nvarchar(24)")]
        public AllergyIntensity Intensity { get; set; }

        [ForeignKey("Allergy")]
        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }
    }
}