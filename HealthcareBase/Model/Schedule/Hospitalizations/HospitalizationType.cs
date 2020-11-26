// File:    HospitalizationType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class HospitalizationType

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.HospitalResources;
using Repository.Generics;

namespace Model.Schedule.Hospitalizations
{
    public class HospitalizationType : Entity<int>
    {
        //public IEnumerable<Department> AppropriateDepartments { get; set; }
        public IEnumerable<EquipmentType> NecessaryEquipment { get; set; }

        [Key]
        public int Id { get; set; }
        public int UsualNumberOfDays { get; set; }
        public string Name { get; set; }
        public int GetKey()
        {
            return Id;
        }
        public void SetKey(int id)
        {
            Id = id;
        }
    }
}