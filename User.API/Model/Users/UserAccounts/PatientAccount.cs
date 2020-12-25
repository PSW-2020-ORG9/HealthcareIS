// File:    PatientAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:46
// Purpose: Definition of Class PatientAccount


using System.ComponentModel.DataAnnotations.Schema;
using User.API.Model.Users.Patients;

namespace User.API.Model.Users.UserAccounts
{
    public class PatientAccount : UserAccount
    {
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public bool RespondedToSurvey { get; set; }


    }
}