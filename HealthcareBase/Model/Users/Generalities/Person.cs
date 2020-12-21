// File:    Person.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Person

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Generalities
{
    public class Person : IEntity<string>
    {
        [Key]
        public string Jmbg { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public IEnumerable<Citizenship> Citizenships { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        
        [Column(TypeName = "nvarchar(24)")]
        public MaritalStatus MaritalStatus { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public Gender Gender { get; set; }
        
        [ForeignKey("CityOfResidence")]
        public int CityOfResidenceId { get; set; }
        public City CityOfResidence { get; set; }
        
        [ForeignKey("CityOfBirth")]
        public int CityOfBirthId { get; set; }
        public City CityOfBirth { get; set; }

        public string GetKey() => Jmbg;
        public void SetKey(string id) => Jmbg = id;
    }
}