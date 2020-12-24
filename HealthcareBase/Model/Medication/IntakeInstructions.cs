// File:    IntakeInstructions.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class IntakeInstructions

using System;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Medication
{
    public class IntakeInstructions : Entity<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesPerDay { get; set; }
        public double Dosage { get; set; }
        public string DosageUnit { get; set; }
        public string Description { get; set; }
    }
}