// File:    Doctor.cs
// Author:  Lana
// Created: 13 April 2020 18:32:18
// Purpose: Definition of Class Doctor

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;

namespace HealthcareBase.Model.Users.Employee
{
    public class Doctor : Employee
    {
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public IEnumerable<Specialty> Specialties { get; set; }
    }
}