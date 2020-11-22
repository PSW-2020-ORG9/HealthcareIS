// File:    Allergy.cs
// Author:  Lana
// Created: 20 April 2020 23:04:50
// Purpose: Definition of Class Allergy

using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.Patient.MedicalHistory;

namespace Model.Miscellaneous
{
    public class Allergy : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Allergen { get; set; }
        public string Prevention { get; set; }
        public string Symptoms { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}