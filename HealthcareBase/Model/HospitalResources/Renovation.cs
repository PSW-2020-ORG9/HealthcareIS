// File:    Renovation.cs
// Author:  Lana
// Created: 21 April 2020 00:07:14
// Purpose: Definition of Class Renovation

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class Renovation : Entity<int>
    {
        public Renovation()
        {
            Room = new Room();
            TimeInterval = new TimeInterval();
        }
     
        public string Description { get; set; }
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}