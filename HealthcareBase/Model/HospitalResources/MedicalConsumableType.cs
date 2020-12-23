// File:    MedicalConsumableType.cs
// Author:  Lana
// Created: 18 April 2020 17:38:58
// Purpose: Definition of Class MedicalConsumableType

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class MedicalConsumableType : IEntity<int>
    {
        public MedicalConsumableType()
        {
        }
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}