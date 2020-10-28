// File:    Allergy.cs
// Author:  Lana
// Created: 20 April 2020 23:04:50
// Purpose: Definition of Class Allergy

using System;
using System.Collections.Generic;

namespace Model.Miscellaneous
{
    public class Allergy : Repository.Generics.Entity<int>
    {
        private String allergen;
        private String symptoms;
        private String prevention;
        private int id;

        public Allergy(string allergen, string symptoms, string prevention)
        {
            this.allergen = allergen;
            this.symptoms = symptoms;
            this.prevention = prevention;
        }

        public Allergy()
        {
        }

        public string Allergen { get => allergen; set => allergen = value; }
        public string Prevention { get => prevention; set => prevention = value; }
        public string Symptoms { get => symptoms; set => symptoms = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is Allergy allergy &&
                   id == allergy.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}