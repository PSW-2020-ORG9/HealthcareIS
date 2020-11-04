// File:    FamilyHistory.cs
// Author:  Gudli
// Created: 21 April 2020 15:19:06
// Purpose: Definition of Class FamilyHistory

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class FamilyHistory
    {
        private List<FamilyMemberDiagnosis> diagnoses;

        public string Overview { get; set; }

        public IEnumerable<FamilyMemberDiagnosis> Diagnoses
        {
            get
            {
                if (diagnoses == null)
                    diagnoses = new List<FamilyMemberDiagnosis>();
                return diagnoses;
            }
            set
            {
                RemoveAllDiagnoses();
                if (value != null)
                    foreach (var diagnosis in value)
                        AddDiagnosis(diagnosis);
            }
        }

        public void AddDiagnosis(FamilyMemberDiagnosis diagnosis)
        {
            if (diagnosis == null)
                return;
            if (diagnoses == null)
                diagnoses = new List<FamilyMemberDiagnosis>();
            if (!diagnoses.Contains(diagnosis))
                diagnoses.Add(diagnosis);
        }

        public void RemoveDiagnosis(FamilyMemberDiagnosis diagnosis)
        {
            if (diagnosis == null)
                return;
            if (diagnoses != null)
                if (diagnoses.Contains(diagnosis))
                    diagnoses.Remove(diagnosis);
        }

        public void RemoveAllDiagnoses()
        {
            if (diagnoses != null)
                diagnoses.Clear();
        }
    }
}