// File:    Shift.cs
// Author:  Lana
// Created: 21 April 2020 00:09:43
// Purpose: Definition of Class Shift

using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;

namespace Model.Users.Employee
{
    public class Shift : Entity<int>
    {
        public TimeInterval TimeInterval { get; set; }

        public Room AssignedExamRoom { get; set; }

        public Doctor Doctor { get; set; }

        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Shift shift &&
                   Id == shift.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}