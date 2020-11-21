// File:    MedicalHistory.cs
// Author:  Gudli
// Created: 20 April 2020 21:21:26
// Purpose: Definition of Class MedicalHistory

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class MedicalHistory
    {
        private List<AllergyManifestation> allergies;

        public MedicalHistory()
        {
            PersonalHistory = new PersonalHistory();
            FamilyHistory = new FamilyHistory();
        }

        public PersonalHistory PersonalHistory { get; set; }

        public FamilyHistory FamilyHistory { get; set; }

        public IEnumerable<AllergyManifestation> Allergies
        {
            get
            {
                if (allergies == null)
                    allergies = new List<AllergyManifestation>();
                return allergies;
            }
            set
            {
                RemoveAllAllergies();
                if (value != null)
                    foreach (var allergy in value)
                        AddAllergy(allergy);
            }
        }

        public void AddAllergy(AllergyManifestation allergy)
        {
            if (allergy == null)
                return;
            if (allergies == null)
                allergies = new List<AllergyManifestation>();
            if (!allergies.Contains(allergy))
                allergies.Add(allergy);
        }

        public void RemoveAllergy(AllergyManifestation allergy)
        {
            if (allergy == null)
                return;
            if (allergies != null)
                if (allergies.Contains(allergy))
                    allergies.Remove(allergy);
        }

        public void RemoveAllAllergies()
        {
            if (allergies != null)
                allergies.Clear();
        }
    }
}