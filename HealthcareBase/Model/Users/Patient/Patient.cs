// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Patient
{
    public class Patient : Entity<int>
    {
        public string InsuranceNumber { get; set; }

        [ForeignKey("Person")]
        public string Jmbg { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public PatientStatus Status { get; set; }
        public IEnumerable<Examination> Examinations { get; set; }
        public IEnumerable<AllergyManifestation> Allergies { get; set; }
    }
}