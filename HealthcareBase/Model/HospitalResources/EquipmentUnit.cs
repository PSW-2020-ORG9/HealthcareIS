// File:    EquipmentUnit.cs
// Author:  Lana
// Created: 18 April 2020 17:00:15
// Purpose: Definition of Class EquipmentUnit

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class EquipmentUnit : Entity<int>
    {
        public DateTime AcquisitionDate { get; set; }
        public string Manufacturer { get; set; }

        [ForeignKey("CurrentLocation")]
        public int? CurrentLocationId { get; set; }
        public Room CurrentLocation { get; set; }

        [ForeignKey("EquipmentType")]
        public int EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}