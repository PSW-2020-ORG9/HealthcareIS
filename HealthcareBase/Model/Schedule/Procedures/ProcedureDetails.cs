// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.HospitalResources;
using Model.Users.Employee;
using Repository.Generics;

namespace Model.Schedule.Procedures
{
    public class ProcedureDetails : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        
        [Column(TypeName = "nvarchar(24)")]
        public ProcedurePriority Priority { get; set; }

        [ForeignKey("RequiredSpecialty")]
        public int RequiredSpecialtyId { get; set; }
        public Specialty RequiredSpecialty { get; set; }
        
        // TODO Required equipment via relationship table 
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}