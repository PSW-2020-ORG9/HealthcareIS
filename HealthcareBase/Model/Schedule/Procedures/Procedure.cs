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
        protected bool avoidChangingDoctor;
        protected bool avoidChangingRoom;
        protected bool avoidChangingTime;
        protected Doctor doctor;
        protected int id;
        protected Patient patient;
        protected ProcedurePriority priority;
        protected ProcedureType procedureType;
        protected Examination referredFrom;
        protected Room room;
        protected TimeInterval timeInterval;

        public TimeInterval TimeInterval
        {
            get => timeInterval;
            set => timeInterval = value;
        }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor
        {
            get => doctor;
            set => doctor = value;
        }

        [ForeignKey("ProcedureType")]
        public int ProcedureTypeId { get; set; }
        public ProcedureType ProcedureType
        {
            get => procedureType;
            set => procedureType = value;
        }

        [Key]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room
        {
            get => room;
            set => room = value;
        }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient
        {
            get => patient;
            set => patient = value;
        }

        [ForeignKey("ReferredFrom")]
        public int ReferredFromId { get; set; }
        public Examination ReferredFrom
        {
            get => referredFrom;
            set => referredFrom = value;
        }

        [Column(TypeName = "nvarchar(24)")]
        public ProcedurePriority Priority
        {
            get => priority;
            set => priority = value;
        }

        public bool AvoidChangingTime
        {
            get => avoidChangingTime;
            set => avoidChangingTime = value;
        }

        public bool AvoidChangingRoom
        {
            get => avoidChangingRoom;
            set => avoidChangingRoom = value;
        }

        public bool AvoidChangingDoctor
        {
            get => avoidChangingDoctor;
            set => avoidChangingDoctor = value;
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Procedure procedure &&
                   id == procedure.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}