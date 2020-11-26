// File:    EquipmentType.cs
// Author:  Lana
// Created: 18 April 2020 16:54:14
// Purpose: Definition of Class EquipmentType

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class EquipmentType : IEntity<int>
    {       
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public bool RequiresRenovationToMove { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}