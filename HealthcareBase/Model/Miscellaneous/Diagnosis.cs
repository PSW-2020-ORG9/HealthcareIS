// File:    Diagnosis.cs
// Author:  Lana
// Created: 20 April 2020 23:15:25
// Purpose: Definition of Class Diagnosis

using System;
using System.Collections.Generic;

namespace Model.Miscellaneous
{
    public class Diagnosis : Repository.Generics.Entity<String>
    {
        private String icd;
        private String name;
        private String description;
        private Boolean isInjury;

        public Diagnosis(string icd, string name, string description, bool isInjury)
        {
            this.icd = icd;
            this.name = name;
            this.description = description;
            this.isInjury = isInjury;
        }

        public Diagnosis()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool IsInjury { get => isInjury; set => isInjury = value; }

        public String Icd { get => icd; set => icd = value; }

        public string GetKey()
        {
            return icd;
        }

        public void SetKey(string id)
        {
            this.icd = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Diagnosis diagnosis &&
                   icd == diagnosis.icd;
        }

        public override int GetHashCode()
        {
            return -1990239221 + EqualityComparer<string>.Default.GetHashCode(icd);
        }
    }
}