// File:    Procedure.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Procedure

using Model.HospitalResources;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Schedule.Procedures
{
    public abstract class Procedure : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public TimeInterval TimeInterval { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        
        [ForeignKey("ProcedureType")]
        public int ProcedureDetailsId { get; set; }
        public ProcedureDetails ProcedureDetails { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [ForeignKey("ReferredFrom")]
        public int? ReferredFromId { get; set; }
        public Examination ReferredFrom { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public ProcedurePriority Priority { get; set; }

        public bool AvoidChangingTime { get; set; }
        public bool AvoidChangingRoom { get; set; }
        public bool AvoidChangingDoctor { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}