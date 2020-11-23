// File:    UserFeedback.cs
// Author:  Lana
// Created: 21 April 2020 18:20:24
// Purpose: Definition of Class UserFeedback

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Model.Users.UserFeedback
{
    public class UserFeedback : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserComment { get; set; }
        public bool isPublic { get; set; }
        public bool isAnonymous { get; set; }
        public bool isPublished { get; set; }

        [ForeignKey("PatientAccount")]
        public int PatientAccountId { get; set; }
        public PatientAccount PatientAccount { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}