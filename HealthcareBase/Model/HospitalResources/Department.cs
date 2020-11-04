// File:    Department.cs
// Author:  Lana
// Created: 18 April 2020 17:20:00
// Purpose: Definition of Class Department

using Repository.Generics;
using System.ComponentModel.DataAnnotations;

namespace Model.HospitalResources
{
    public class Department : Entity<int>
    {
        public Department()
        {
        }

        public Department(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        [Key]
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
            return obj is Department department &&
                   Id == department.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}