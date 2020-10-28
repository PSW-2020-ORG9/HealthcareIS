// File:    MedicationInputRequest.cs
// Author:  Lana
// Created: 27 May 2020 20:29:45
// Purpose: Definition of Class MedicationInputRequest

using Model.Users.Employee;
using System;
using System.Collections.Generic;

namespace Model.Requests
{
    public class MedicationInputRequest : Request
    {
        private List<Specialty> reviewableBy;
        private Medication.Medication medication;

        public Medication.Medication Medication { get => medication; set => medication = value; }

        public MedicationInputRequest()
        {
            medication = new Medication.Medication();
        }

        public IEnumerable<Specialty> ReviewableBy
        {
            get
            {
                if (reviewableBy == null)
                    reviewableBy = new List<Specialty>();
                return reviewableBy;
            }
            set
            {
                RemoveAllSpecialties();
                if (value != null)
                {
                    foreach (Specialty oSpecialty in value)
                        AddSpecialties(oSpecialty);
                }
            }
        }

        public void AddSpecialties(Specialty newSpecialty)
        {
            if (newSpecialty == null)
                return;
            if (this.reviewableBy == null)
                this.reviewableBy = new List<Specialty>();
            if (!this.reviewableBy.Contains(newSpecialty))
                this.reviewableBy.Add(newSpecialty);
        }

        public void RemoveSpecialties(Specialty oldSpecialty)
        {
            if (oldSpecialty == null)
                return;
            if (this.reviewableBy != null)
                if (this.reviewableBy.Contains(oldSpecialty))
                    this.reviewableBy.Remove(oldSpecialty);
        }

        public void RemoveAllSpecialties()
        {
            if (reviewableBy != null)
                reviewableBy.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is MedicationInputRequest request &&
                   id == request.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}