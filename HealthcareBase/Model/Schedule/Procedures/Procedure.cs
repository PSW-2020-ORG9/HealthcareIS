// File:    Procedure.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Procedure

using Model.HospitalResources;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using System;

namespace Model.Schedule.Procedures
{
    public abstract class Procedure : Repository.Generics.Entity<int>
    {
        protected TimeInterval timeInterval;
        protected Doctor doctor;
        protected Room room;
        protected ProcedureType procedureType;
        protected Patient patient;
        protected Examination referredFrom;
        protected ProcedurePriority priority;
        protected Boolean avoidChangingTime;
        protected Boolean avoidChangingRoom;
        protected Boolean avoidChangingDoctor;
        protected int id;

        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public ProcedureType ProcedureType { get => procedureType; set => procedureType = value; }
        public int Id { get => id; set => id = value; }
        public Room Room { get => room; set => room = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Examination ReferredFrom { get => referredFrom; set => referredFrom = value; }
        public ProcedurePriority Priority { get => priority; set => priority = value; }
        public bool AvoidChangingTime { get => avoidChangingTime; set => avoidChangingTime = value; }
        public bool AvoidChangingRoom { get => avoidChangingRoom; set => avoidChangingRoom = value; }
        public bool AvoidChangingDoctor { get => avoidChangingDoctor; set => avoidChangingDoctor = value; }

        public override bool Equals(object obj)
        {
            return obj is Procedure procedure &&
                   id == procedure.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}