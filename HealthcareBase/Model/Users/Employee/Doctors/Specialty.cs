// File:    Specialty.cs
// Author:  Lana
// Created: 13 April 2020 18:40:05
// Purpose: Definition of Class Specialty

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Employee.Doctors
{
    public class Specialty : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}