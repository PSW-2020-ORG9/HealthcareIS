// File:    MedicalHistory.cs
// Author:  Gudli
// Created: 20 April 2020 21:21:26
// Purpose: Definition of Class MedicalHistory

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Patient.MedicalHistory
{
    public class MedicalRecord : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public IEnumerable<AllergyManifestation> Allergies { get; set; }
        public IEnumerable<Examination> Examinations { get; set; }
        public IEnumerable<Surgery> Surgeries { get; set; }

        public IEnumerable<FamilyMemberDiagnosis> FamilyMemberDiagnoses { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}