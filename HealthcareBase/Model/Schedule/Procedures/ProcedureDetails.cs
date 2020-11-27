// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class ProcedureDetails : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        
        [Column(TypeName = "nvarchar(12)")]
        public ProcedurePriority Priority { get; set; }

        [ForeignKey("RequiredSpecialty")]
        public int RequiredSpecialtyId { get; set; }
        public Specialty RequiredSpecialty { get; set; }
        
        // TODO Required equipment via relationship table 
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}