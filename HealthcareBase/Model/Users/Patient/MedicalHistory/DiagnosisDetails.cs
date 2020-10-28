// File:    DiagnosisDetails.cs
// Author:  Gudli
// Created: 21 April 2020 15:39:06
// Purpose: Definition of Class DiagnosisDetails

using Model.Miscellaneous;

namespace Model.Users.Patient.MedicalHistory
{
    public class DiagnosisDetails
    {
        public int DiscoveredAtAge { get; set; }

        public ConditionType Type { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}