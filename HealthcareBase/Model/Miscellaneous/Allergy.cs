// File:    Allergy.cs
// Author:  Lana
// Created: 20 April 2020 23:04:50
// Purpose: Definition of Class Allergy

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Miscellaneous
{
    public class Allergy : Entity<int>
    {
        public string Allergen { get; set; }
        public string Prevention { get; set; }
        public string Symptoms { get; set; }
    }
}