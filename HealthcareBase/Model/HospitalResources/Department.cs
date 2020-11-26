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
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}