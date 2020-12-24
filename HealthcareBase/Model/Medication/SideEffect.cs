// File:    SideEffect.cs
// Author:  Lana
// Created: 14 April 2020 20:46:07
// Purpose: Definition of Class SideEffect

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Medication
{
    public class SideEffect : Entity<int>
    {
        public string Name { get; set; }
        public int MedicationId { get; set; }
    }
}