// File:    AllergyManifestation.cs
// Author:  Gudli
// Created: 20 April 2020 17:03:34
// Purpose: Definition of Class AllergyManifestation

using Model.Miscellaneous;
using System;

namespace Model.Users.Patient.MedicalHistory
{
    public class AllergyManifestation
    {
        private AllergyIntensity intensity;
        private Allergy allergy;

        public AllergyIntensity Intensity { get => intensity; set => intensity = value; }
        public Allergy Allergy { get => allergy; set => allergy = value; }
    }
}