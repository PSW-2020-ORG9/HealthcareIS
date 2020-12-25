// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

#nullable enable
using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;
using User.API.Model.Generalities;

namespace User.API.Model.Users.Employees
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