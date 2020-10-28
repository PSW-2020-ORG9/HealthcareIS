// File:    Renovation.cs
// Author:  Lana
// Created: 21 April 2020 00:07:14
// Purpose: Definition of Class Renovation

using Model.Utilities;
using System;

namespace Model.HospitalResources
{
    public class Renovation : Repository.Generics.Entity<int>
    {
        private String description;
        private TimeInterval timeInterval;
        private Room room;
        private int id;

        public Renovation(string description, TimeInterval timeInterval, Room room)
        {
            this.description = description;
            this.timeInterval = timeInterval;
            this.room = room;
        }

        public Renovation()
        {
            room = new Room();
            timeInterval = new TimeInterval();
        }

        public string Description { get => description; set => description = value; }
        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Room Room { get => room; set => room = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is Renovation renovation &&
                   id == renovation.id;
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