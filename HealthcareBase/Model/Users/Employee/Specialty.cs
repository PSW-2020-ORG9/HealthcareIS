// File:    Specialty.cs
// Author:  Lana
// Created: 13 April 2020 18:40:05
// Purpose: Definition of Class Specialty

using System;

namespace Model.Users.Employee
{
    public class Specialty : Repository.Generics.Entity<int>
    {
        private String name;
        private String description;
        private int id;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is Specialty specialty &&
                   id == specialty.id;
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