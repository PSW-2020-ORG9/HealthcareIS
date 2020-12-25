// File:    Procedure.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Procedure

using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;
using User.API.Model.HospitalResources;
using User.API.Model.Users.Employees.Doctors;
using User.API.Model.Users.Patients;
using User.API.Model.Utilities;

namespace User.API.Model.Schedule
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