// File:    BlogAuthor.cs
// Author:  Gudli
// Created: 20 April 2020 11:54:01
// Purpose: Definition of Class BlogAuthor

using Model.Users.Employee;
using System;

namespace Model.Blog
{
    public class BlogAuthor : Repository.Generics.Entity<int>
    {
        private string description;
        private Doctor doctor;
        private int id;

        public BlogAuthor(string description, Doctor doctor)
        {
            this.description = description;
            this.doctor = doctor;
        }

        public BlogAuthor()
        {
            doctor = new Doctor();
        }

        public string Description { get => description; set => description = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is BlogAuthor author &&
                   id == author.id;
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