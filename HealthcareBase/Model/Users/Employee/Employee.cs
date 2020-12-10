// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Employee
{
    public abstract class Employee : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Person")]
        public string Jmbg { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public EmployeeStatus Status { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
        public override bool Equals(object? obj)
        {
            if (!(obj is Employee employee))
                return false;
            return Id.Equals(employee.Id);
        }
    }
}