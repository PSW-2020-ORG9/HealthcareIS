// File:    IntakeInstructions.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class IntakeInstructions

using System;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Medication
{
    [Owned]
    public class IntakeInstructions
    {
        public IntakeInstructions(DateTime startDate, DateTime endDate, int timesPerDay, double dosage,
            string dosageUnit, string description)
        {
            StartDate = startDate;
            EndDate = endDate;
            TimesPerDay = timesPerDay;
            Dosage = dosage;
            DosageUnit = dosageUnit;
            Description = description;
        }

        public IntakeInstructions()
        {
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TimesPerDay { get; set; }

        public double Dosage { get; set; }

        public string DosageUnit { get; set; }

        public string Description { get; set; }
    }
}