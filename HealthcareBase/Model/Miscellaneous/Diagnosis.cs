// File:    Diagnosis.cs
// Author:  Lana
// Created: 20 April 2020 23:15:25
// Purpose: Definition of Class Diagnosis

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Miscellaneous
{
    public class Diagnosis : Entity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInjury { get; set; }
    }
}