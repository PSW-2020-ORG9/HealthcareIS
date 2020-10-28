// File:    Notification.cs
// Author:  Lana
// Created: 27 May 2020 20:38:16
// Purpose: Definition of Class Notification

using Model.Users.UserAccounts;
using System;

namespace Model.Notifications
{
    public abstract class Notification : Repository.Generics.Entity<int>
    {
        private int id;
        private Boolean read;
        private Model.Users.UserAccounts.UserAccount user;

        public bool Read { get => read; set => read = value; }
        public UserAccount User { get => user; set => user = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is Notification notification &&
                   id == notification.id;
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