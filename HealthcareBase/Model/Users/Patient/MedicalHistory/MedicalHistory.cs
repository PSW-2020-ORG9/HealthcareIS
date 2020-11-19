// File:    MedicalHistory.cs
// Author:  Gudli
// Created: 20 April 2020 21:21:26
// Purpose: Definition of Class MedicalHistory

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class MedicalHistory
    {
        public List<AllergyManifestation> Allergies { get; set; }

        public MedicalHistory()
        {
            PersonalHistory = new PersonalHistory();
            FamilyHistory = new FamilyHistory();
        }

        public PersonalHistory PersonalHistory { get; set; }

        public FamilyHistory FamilyHistory { get; set; }

    }
}