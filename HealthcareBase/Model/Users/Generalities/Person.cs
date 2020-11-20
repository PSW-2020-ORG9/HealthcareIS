// File:    Person.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Person

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Generalities
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Jmbg { get; set; }
        public IEnumerable<Citizenship> Citizenships { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public Gender Gender { get; set; }

        [ForeignKey("CityOfResidence")]
        public int CityOfResidenceId { get; set; }
        public City CityOfResidence { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Now.Date;
                var age = today.Year - DateOfBirth.Year - 1;
                if (today.DayOfYear >= DateOfBirth.DayOfYear)
                    age++;
                return age;
            }
        }
    }
}