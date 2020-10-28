// File:    AllergyManifestation.cs
// Author:  Gudli
// Created: 20 April 2020 17:03:34
// Purpose: Definition of Class AllergyManifestation

using Model.Miscellaneous;

namespace Model.Users.Patient.MedicalHistory
{
    public class AllergyManifestation
    {
        public AllergyIntensity Intensity { get; set; }

        public Allergy Allergy { get; set; }
    }
}