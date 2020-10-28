// File:    Diagnosis.cs
// Author:  Lana
// Created: 20 April 2020 23:15:25
// Purpose: Definition of Class Diagnosis

using System.Collections.Generic;
using Repository.Generics;

namespace Model.Miscellaneous
{
    public class Diagnosis : Entity<string>
    {
        public Diagnosis(string icd, string name, string description, bool isInjury)
        {
            Icd = icd;
            Name = name;
            Description = description;
            IsInjury = isInjury;
        }

        public Diagnosis()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsInjury { get; set; }

        public string Icd { get; set; }

        public string GetKey()
        {
            return Icd;
        }

        public void SetKey(string id)
        {
            Icd = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Diagnosis diagnosis &&
                   Icd == diagnosis.Icd;
        }

        public override int GetHashCode()
        {
            return -1990239221 + EqualityComparer<string>.Default.GetHashCode(Icd);
        }
    }
}