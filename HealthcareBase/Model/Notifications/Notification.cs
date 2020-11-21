// File:    Notification.cs
// Author:  Lana
// Created: 27 May 2020 20:38:16
// Purpose: Definition of Class Notification

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Notifications
{
    public abstract class Notification : Entity<int>
    {
        public bool Read { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
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
            return obj is Notification notification &&
                   Id == notification.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}