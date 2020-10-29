// File:    UserFeedback.cs
// Author:  Lana
// Created: 21 April 2020 18:20:24
// Purpose: Definition of Class UserFeedback

using System;
using System.ComponentModel.DataAnnotations;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Model.Users.UserFeedback
{
    public class UserFeedback : Entity<int>
    {
        public DateTime Date { get; set; }

        public string UserComment { get; set; }

        public UserAccount User { get; set; }

        [Key]
        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is UserFeedback feedback &&
                   Id == feedback.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}