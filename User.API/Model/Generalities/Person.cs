// File:    Person.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Person

using General;
using System;
using System.Collections.Generic;
using User.API.Infrastructure;
using User.API.Model.Locale;

namespace User.API.Model.Generalities
{
    public class Person : Entity<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public IEnumerable<Citizenship> Citizenships { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        
        public MaritalStatus MaritalStatus { get; set; }
        public Gender Gender { get; set; }
        public int CityOfResidenceId { get; set; }
        public City CityOfResidence { get; set; }
        public int CityOfBirthId { get; set; }
        public City CityOfBirth { get; set; }
    }
}