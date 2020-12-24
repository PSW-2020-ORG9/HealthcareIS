// File:    Procedure.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Procedure

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public abstract class Procedure : Entity<int>
    {
        public TimeInterval TimeInterval { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        
        [ForeignKey("ProcedureDetails")]
        public int? ProcedureDetailsId { get; set; }
        public ProcedureDetails ProcedureDetails { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [ForeignKey("ReferredFrom")]
        public int? ReferredFromId { get; set; }
        public Examination ReferredFrom { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public ProcedurePriority Priority { get; set; }
    }
}