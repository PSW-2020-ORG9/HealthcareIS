// File:    Specialty.cs
// Author:  Lana
// Created: 13 April 2020 18:40:05
// Purpose: Definition of Class Specialty

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Employee.Doctors
{
    public class Specialty : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}