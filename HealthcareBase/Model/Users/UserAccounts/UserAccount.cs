// File:    UserAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:09
// Purpose: Definition of Class UserAccount

using Repository.Generics;
using System.ComponentModel.DataAnnotations;

namespace Model.Users.UserAccounts
{
    public abstract class UserAccount : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}