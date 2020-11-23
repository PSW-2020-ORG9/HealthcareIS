// File:    Ingredient.cs
// Author:  Lana
// Created: 14 April 2020 20:43:24
// Purpose: Definition of Class Ingredient

using System.ComponentModel.DataAnnotations;
using Repository.Generics;

namespace Model.Medication
{
    public class Ingredient : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAllergen { get; set; }
        public int MedicationId { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}