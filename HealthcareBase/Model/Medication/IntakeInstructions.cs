// File:    IntakeInstructions.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class IntakeInstructions

using System;

namespace Model.Medication
{
    public class IntakeInstructions
    {
        private DateTime startDate;
        private DateTime endDate;
        private int timesPerDay;
        private double dosage;
        private String dosageUnit;
        private String description;

        public IntakeInstructions(DateTime startDate, DateTime endDate, int timesPerDay, double dosage, string dosageUnit, string description)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.timesPerDay = timesPerDay;
            this.dosage = dosage;
            this.dosageUnit = dosageUnit;
            this.description = description;
        }

        public IntakeInstructions()
        {
        }

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public int TimesPerDay { get => timesPerDay; set => timesPerDay = value; }
        public double Dosage { get => dosage; set => dosage = value; }
        public string DosageUnit { get => dosageUnit; set => dosageUnit = value; }
        public string Description { get => description; set => description = value; }
    }
}