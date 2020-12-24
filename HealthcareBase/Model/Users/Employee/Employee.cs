// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Employee
{
    public abstract class Employee : Entity<int>
    {
        [ForeignKey("Person")]
        public string Jmbg { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public EmployeeStatus Status { get; set; }
    }
}