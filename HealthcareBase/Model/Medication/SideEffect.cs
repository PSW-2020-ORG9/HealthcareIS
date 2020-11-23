// File:    SideEffect.cs
// Author:  Lana
// Created: 14 April 2020 20:46:07
// Purpose: Definition of Class SideEffect

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Repository.Generics;

namespace Model.Medication
{
    public class SideEffect : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MedicationId { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}