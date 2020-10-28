// File:    PersonalHistory.cs
// Author:  Gudli
// Created: 21 April 2020 15:36:43
// Purpose: Definition of Class PersonalHistory

using Model.Miscellaneous;
using System;
using System.Collections.Generic;

namespace Model.Users.Patient.MedicalHistory
{
    public class PersonalHistory
    {
        private String overview;
        private List<DiagnosisDetails> diagnoses;

        public string Overview { get => overview; set => overview = value; }

        public IEnumerable<DiagnosisDetails> Diagnoses
        {
            get
            {
                if (diagnoses == null)
                    diagnoses = new List<DiagnosisDetails>();
                return diagnoses;
            }
            set
            {
                RemoveAllDiagnoses();
                if (value != null)
                {
                    foreach (DiagnosisDetails diagnosis in value)
                        AddDiagnosis(diagnosis);
                }
            }
        }

        public void AddDiagnosis(DiagnosisDetails diagnosis)
        {
            if (diagnosis == null)
                return;
            if (diagnoses == null)
                diagnoses = new List<DiagnosisDetails>();
            if (!diagnoses.Contains(diagnosis))
                diagnoses.Add(diagnosis);
        }

        public void RemoveDiagnosis(DiagnosisDetails diagnosis)
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