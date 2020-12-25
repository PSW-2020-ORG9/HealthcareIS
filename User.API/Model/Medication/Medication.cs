// File:    Medication.cs
// Author:  Lana
// Created: 14 April 2020 20:48:37
// Purpose: Definition of Class Medication

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;

namespace User.API.Model.Medication
{
    public class Medication : Entity<int>
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public MedicineType Type { get; set; }

        public IEnumerable<SideEffect> SideEffects { get; set; }
        public IEnumerable<User.API.Model.Medication.Ingredient> Ingredients { get; set; }
    }
}