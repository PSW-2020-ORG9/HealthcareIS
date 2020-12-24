// File:    PatientAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:46
// Purpose: Definition of Class PatientAccount
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HealthcareBase.Model.Users.Employee.Doctors;

namespace HealthcareBase.Model.Users.UserAccounts
{
    public class PatientAccount : UserAccount
    {
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient.Patient Patient { get; set; }
        public bool RespondedToSurvey { get; set; }


    }
}