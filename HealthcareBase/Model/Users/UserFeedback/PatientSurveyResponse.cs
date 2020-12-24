// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.UserFeedback
{
    public class PatientSurveyResponse : Entity<int>
    {
        public int ExperienceRating { get; set; }

        [ForeignKey("BestDoctor")]
        public int BestDoctorId { get; set; }
        public Doctor BestDoctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public PatientAccount Patient { get; set; }
    }
}