// File:    FamilyMember.cs
// Author:  Gudli
// Created: 21 April 2020 15:23:25
// Purpose: Definition of Class FamilyMember

using Model.Miscellaneous;
using System;

namespace Model.Users.Patient.MedicalHistory
{
    public class FamilyMemberDiagnosis
    {
        private String familyRelation;
        private int discoveredAtAge;
        private bool lethal;
        private Diagnosis diagnosis;

        public string FamilyRelation { get => familyRelation; set => familyRelation = value; }
        public int DiscoveredAtAge { get => discoveredAtAge; set => discoveredAtAge = value; }
        public bool Lethal { get => lethal; set => lethal = value; }
        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
    }
}