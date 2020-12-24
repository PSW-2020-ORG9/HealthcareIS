// File:    UserAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:09
// Purpose: Definition of Class UserAccount

using System;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Repository.Generics;
using ValidationException = HealthcareBase.Model.CustomExceptions.ValidationException;

namespace HealthcareBase.Model.Users.UserAccounts
{
    public abstract class UserAccount : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public Credentials Credentials { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsActivated { get; set; }
        public Guid UserGuid { get; set; }
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
        public void ActivateAccount()
        {
            if(IsActivated) throw new ValidationException("Account is already activated.");
            IsActivated = true;
        }
    }
}