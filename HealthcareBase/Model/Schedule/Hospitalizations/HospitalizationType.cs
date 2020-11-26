// File:    HospitalizationType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class HospitalizationType

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Hospitalizations
{
    public class HospitalizationType : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int UsualNumberOfDays { get; set; }
        public string Name { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}