// File:    Renovation.cs
// Author:  Lana
// Created: 21 April 2020 00:07:14
// Purpose: Definition of Class Renovation

using Model.Utilities;
using Repository.Generics;

namespace Model.HospitalResources
{
    public class Renovation : Entity<int>
    {
        public Renovation(string description, TimeInterval timeInterval, Room room)
        {
            Description = description;
            TimeInterval = timeInterval;
            Room = room;
        }

        public Renovation()
        {
            Room = new Room();
            TimeInterval = new TimeInterval();
        }

        public string Description { get; set; }

        public TimeInterval TimeInterval { get; set; }

        public Room Room { get; set; }

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
            return obj is Renovation renovation &&
                   Id == renovation.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}