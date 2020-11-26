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

        public MedicalConsumableType(string name, string purpose)
        {
            Name = name;
            Purpose = purpose;
        }

        public string Name { get; set; }

        public string Purpose { get; set; }

        [Key]
        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is MedicalConsumableType type &&
                   Id == type.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}