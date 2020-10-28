// File:    UserAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:09
// Purpose: Definition of Class UserAccount

using System;

namespace Model.Users.UserAccounts
{
    public abstract class UserAccount : Repository.Generics.Entity<int>
    {
        protected String username;
        protected String password;
        protected int id;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is UserAccount account &&
                   id == account.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}