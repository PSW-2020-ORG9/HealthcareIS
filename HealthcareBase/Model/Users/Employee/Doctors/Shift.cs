// File:    Shift.cs
// Author:  Lana
// Created: 21 April 2020 00:09:43
// Purpose: Definition of Class Shift

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Employee.Doctors
{
    public class Shift : IEntity<int>
    {
        public TimeInterval TimeInterval { get; set; }
        

        [ForeignKey("AssignedExamRoom")]
        public int AssignedExamRoomId { get; set; }
        public Room AssignedExamRoom { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Key]
        public int Id { get; set; }
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}