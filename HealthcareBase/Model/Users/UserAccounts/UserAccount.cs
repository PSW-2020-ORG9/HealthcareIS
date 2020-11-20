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
        // TODO Employees and Patients are stored in the same table when using this hierarchy.
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;

        public override bool Equals(object obj)
            => obj is UserAccount account &&
               Id == account.Id;
    }
}