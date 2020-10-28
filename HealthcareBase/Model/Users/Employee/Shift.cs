// File:    Shift.cs
// Author:  Lana
// Created: 21 April 2020 00:09:43
// Purpose: Definition of Class Shift

using Model.HospitalResources;
using Model.Utilities;
using System;

namespace Model.Users.Employee
{
    public class Shift : Repository.Generics.Entity<int>
    {
        private TimeInterval timeInterval;
        private Room assignedExamRoom;
        private Doctor doctor;
        private int id;

        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Room AssignedExamRoom { get => assignedExamRoom; set => assignedExamRoom = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is Shift shift &&
                   id == shift.id;
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