// File:    BlogAuthor.cs
// Author:  Gudli
// Created: 20 April 2020 11:54:01
// Purpose: Definition of Class BlogAuthor

using Model.Users.Employee;
using Repository.Generics;

namespace Model.Blog
{
    public class BlogAuthor : Entity<int>
    {
        public BlogAuthor(string description, Doctor doctor)
        {
            Description = description;
            Doctor = doctor;
        }

        public BlogAuthor()
        {
            Doctor = new Doctor();
        }

        public string Description { get; set; }

        public Doctor Doctor { get; set; }

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
            return obj is BlogAuthor author &&
                   Id == author.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}