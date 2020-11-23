// File:    Medication.cs
// Author:  Lana
// Created: 14 April 2020 20:48:37
// Purpose: Definition of Class Medication

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Generics;

namespace Model.Medication
{
    public class Medication : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public MedicineType Type { get; set; }

        public IEnumerable<SideEffect> SideEffects { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}