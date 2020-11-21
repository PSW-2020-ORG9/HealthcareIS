// File:    Allergy.cs
// Author:  Lana
// Created: 20 April 2020 23:04:50
// Purpose: Definition of Class Allergy

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Miscellaneous
{
    public class Allergy : Entity<int>
    {
        public Allergy(string allergen, string symptoms, string prevention)
        {
            Allergen = allergen;
            Symptoms = symptoms;
            Prevention = prevention;
        }

        public Allergy()
        {
        }

        public string Allergen { get; set; }

        public string Prevention { get; set; }

        public string Symptoms { get; set; }

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
            return obj is Allergy allergy &&
                   Id == allergy.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}