// File:    Department.cs
// Author:  Lana
// Created: 18 April 2020 17:20:00
// Purpose: Definition of Class Department

using System;
using System.Security.Cryptography;

namespace Model.HospitalResources
{
    public class Department : Repository.Generics.Entity<int>
    {
        private String name;
        private String description;
        private int id;

        public Department()
        {

        }

        public Department(String name, String description)
        {
            this.name = name;
            this.description = description;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public int Id { get => id; set => id = value; }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Department department &&
                   id == department.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}