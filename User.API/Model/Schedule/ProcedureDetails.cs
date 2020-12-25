// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using System;
using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;
using User.API.Model.Users.Employees.Doctors;

namespace User.API.Model.Schedule
{
    public class ProcedureDetails : Entity<int>
    {
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        
        [Column(TypeName = "nvarchar(12)")]
        public ProcedurePriority Priority { get; set; }

        [ForeignKey("RequiredSpecialty")]
        public int RequiredSpecialtyId { get; set; }
        public Specialty RequiredSpecialty { get; set; }
    }
}