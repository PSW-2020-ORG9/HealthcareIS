// File:    Diagnosis.cs
// Author:  Lana
// Created: 20 April 2020 23:15:25
// Purpose: Definition of Class Diagnosis

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Generics;

namespace Model.Miscellaneous
{
    public class Diagnosis : Entity<string>
    {
        [Key]
        public string Icd { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInjury { get; set; }
        
        public string GetKey() => Icd;
        public void SetKey(string icd) => Icd = icd;
    }
}