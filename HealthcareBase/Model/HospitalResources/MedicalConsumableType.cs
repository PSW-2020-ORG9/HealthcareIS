// File:    MedicalConsumableType.cs
// Author:  Lana
// Created: 18 April 2020 17:38:58
// Purpose: Definition of Class MedicalConsumableType

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.HospitalResources
{
    public class MedicalConsumableType : Entity<int>
    {
        public MedicalConsumableType()
        {
        }
        
        public string Name { get; set; }
        public string Purpose { get; set; }
    }
}