// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;
using User.API.Model.Generalities;
using User.API.Model.Schedule;
using User.API.Model.Users.Patients.MedicalHistory.Relationship;

namespace User.API.Model.Users.Patients
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