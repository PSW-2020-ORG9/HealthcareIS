// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class ProcedureType : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public bool SchedulableByPatient { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public ProcedureKind Kind { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public ProcedurePriority Priority { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}