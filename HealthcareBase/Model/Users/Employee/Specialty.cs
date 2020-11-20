// File:    Specialty.cs
// Author:  Lana
// Created: 13 April 2020 18:40:05
// Purpose: Definition of Class Specialty

using Repository.Generics;
using System.ComponentModel.DataAnnotations;

namespace Model.Users.Employee
{
    public class Specialty : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;

        public override bool Equals(object obj)
            => obj is Specialty specialty &&
               Id == specialty.Id;
    }
}