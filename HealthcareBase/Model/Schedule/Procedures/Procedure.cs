// File:    Procedure.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Procedure

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public abstract class Procedure : IEntity<int>
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
        public int ProcedureTypeId { get; set; }
        public ProcedureType ProcedureType { get; set; }

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