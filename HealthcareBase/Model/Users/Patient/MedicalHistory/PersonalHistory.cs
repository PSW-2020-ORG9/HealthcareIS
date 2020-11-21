// File:    PersonalHistory.cs
// Author:  Gudli
// Created: 21 April 2020 15:36:43
// Purpose: Definition of Class PersonalHistory

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Users.Patient.MedicalHistory
{
    [Owned]
    public class PersonalHistory
    {
        private List<DiagnosisDetails> diagnoses;

        public string Overview { get; set; }

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
                    foreach (var diagnosis in value)
                        AddDiagnosis(diagnosis);
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