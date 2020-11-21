// File:    MedicationInputRequest.cs
// Author:  Lana
// Created: 27 May 2020 20:29:45
// Purpose: Definition of Class MedicationInputRequest

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Employee;

namespace HealthcareBase.Model.Requests
{
    public class MedicationInputRequest : Request
    {
        private List<Specialty> reviewableBy;

        public MedicationInputRequest()
        {
            Medication = new Medication.Medication();
        }

        [ForeignKey("Medication")]
        public int MedicationId { get; set; }
        public Medication.Medication Medication { get; set; }

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
                    foreach (var oSpecialty in value)
                        AddSpecialties(oSpecialty);
            }
        }

        public void AddSpecialties(Specialty newSpecialty)
        {
            if (newSpecialty == null)
                return;
            if (reviewableBy == null)
                reviewableBy = new List<Specialty>();
            if (!reviewableBy.Contains(newSpecialty))
                reviewableBy.Add(newSpecialty);
        }

        public void RemoveSpecialties(Specialty oldSpecialty)
        {
            if (oldSpecialty == null)
                return;
            if (reviewableBy != null)
                if (reviewableBy.Contains(oldSpecialty))
                    reviewableBy.Remove(oldSpecialty);
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