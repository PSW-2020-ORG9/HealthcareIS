// File:    MedicalConsumable.cs
// Author:  Lana
// Created: 18 April 2020 17:49:09
// Purpose: Definition of Class MedicalConsumable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class MedicalConsumable : IEntity<int>
    {
        public MedicalConsumable()
        {
            ConsumableType = new MedicalConsumableType();
        }

        [Key]
        public int Id { get; set; }
        
        public string Manufacturer { get; set; }
        public string Description { get; set; }

        [ForeignKey("ConsumableType")]
        public int? ConsumableTypeId { get; set; }
        public MedicalConsumableType ConsumableType { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}