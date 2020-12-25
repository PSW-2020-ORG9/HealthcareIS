// File:    Shift.cs
// Author:  Lana
// Created: 21 April 2020 00:09:43
// Purpose: Definition of Class Shift

using System.ComponentModel.DataAnnotations.Schema;
using User.API.Infrastructure;
using User.API.Model.HospitalResources;
using User.API.Model.Utilities;

namespace User.API.Model.Users.Employees.Doctors
{
    public class Shift : Entity<int>
    {
        public TimeInterval TimeInterval { get; set; }
        

        [ForeignKey("AssignedExamRoom")]
        public int AssignedExamRoomId { get; set; }
        public Room AssignedExamRoom { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}