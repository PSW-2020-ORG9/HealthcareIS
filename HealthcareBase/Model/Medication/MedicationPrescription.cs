// File:    MedicationPrescription.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class MedicationPrescription

using Model.Miscellaneous;
using Model.Users.Employee;
using Model.Users.Patient;
using System;

namespace Model.Medication
{
    public class MedicationPrescription : Repository.Generics.Entity<int>
    {
        private DateTime date;
        private Diagnosis diagnosis;
        private Medication medication;
        private IntakeInstructions instructions;
        private Doctor prescribedBy;
        private Patient patient;
        private int id;

        public MedicationPrescription()
        {
        }

        public DateTime Date { get => date; set => date = value; }
        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
        public Medication Medication { get => medication; set => medication = value; }
        public IntakeInstructions Instructions { get => instructions; set => instructions = value; }
        public Doctor PrescribedBy { get => prescribedBy; set => prescribedBy = value; }
        public Patient Patient { get => patient; set => patient = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is MedicationPrescription prescription &&
                   id == prescription.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}