// File:    UserFeedback.cs
// Author:  Lana
// Created: 21 April 2020 18:20:24
// Purpose: Definition of Class UserFeedback

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.UserFeedback
{
    public class UserFeedback : Entity<int>
    {
        public DateTime Date { get; set; }
        public string UserComment { get; set; }
        public FeedbackVisibility FeedbackVisibility { get; set; }
        [ForeignKey("PatientAccount")]
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }
    }
}