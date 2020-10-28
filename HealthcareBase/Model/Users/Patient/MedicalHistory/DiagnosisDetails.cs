// File:    DiagnosisDetails.cs
// Author:  Gudli
// Created: 21 April 2020 15:39:06
// Purpose: Definition of Class DiagnosisDetails

using Model.Miscellaneous;
using System;

namespace Model.Users.Patient.MedicalHistory
{
    public class DiagnosisDetails
    {
        private int discoveredAtAge;
        private ConditionType type;
        private Diagnosis diagnosis;

        public int DiscoveredAtAge { get => discoveredAtAge; set => discoveredAtAge = value; }
        public ConditionType Type { get => type; set => type = value; }
        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
    }
}