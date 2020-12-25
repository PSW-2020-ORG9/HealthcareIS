// File:    Ingredient.cs
// Author:  Lana
// Created: 14 April 2020 20:43:24
// Purpose: Definition of Class Ingredient


using User.API.Infrastructure;

namespace User.API.Model.Medication
{
    public class Ingredient : Entity<int>
    {
        public string Name { get; set; }
        public bool IsAllergen { get; set; }
        public int MedicationId { get; set; }
    }
}