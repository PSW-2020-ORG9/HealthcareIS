// File:    Department.cs
// Author:  Lana
// Created: 18 April 2020 17:20:00
// Purpose: Definition of Class Department

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class Department : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}