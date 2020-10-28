// File:    UserFeedback.cs
// Author:  Lana
// Created: 21 April 2020 18:20:24
// Purpose: Definition of Class UserFeedback

using Model.Users.UserAccounts;
using Repository.Generics;
using System;

namespace Model.Users.UserFeedback
{
    public class UserFeedback : Entity<int>
    {
        private DateTime date;
        private String userComment;
        private UserAccount user;
        private int id;

        public DateTime Date { get => date; set => date = value; }
        public string UserComment { get => userComment; set => userComment = value; }
        public UserAccount User { get => user; set => user = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is UserFeedback feedback &&
                   id == feedback.id;
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