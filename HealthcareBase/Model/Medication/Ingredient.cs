// File:    Ingredient.cs
// Author:  Lana
// Created: 14 April 2020 20:43:24
// Purpose: Definition of Class Ingredient

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Medication
{
    public class Ingredient : Entity<int>
    {
        public string Name { get; set; }
        public bool IsAllergen { get; set; }
        public int MedicationId { get; set; }
    }
}