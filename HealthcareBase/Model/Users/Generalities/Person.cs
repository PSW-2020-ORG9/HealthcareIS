// File:    Person.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Person

using System;
using System.Collections.Generic;

namespace Model.Users.Generalities
{
    public abstract class Person
    {
        protected String name;
        protected String surname;
        protected DateTime dateOfBirth;
        protected String address;
        protected String telephoneNumber;
        protected String jmbg;
        protected Gender gender;
        protected City cityOfResidence;
        protected List<Country> citizenship;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Address { get => address; set => address = value; }
        public string TelephoneNumber { get => telephoneNumber; set => telephoneNumber = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public City CityOfResidence { get => cityOfResidence; set => cityOfResidence = value; }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Now.Date;
                int age = (today.Year - dateOfBirth.Year - 1);
                if (today.DayOfYear >= dateOfBirth.DayOfYear)
                    age++;
                return age;
            }
        }

        public IEnumerable<Country> Citizenship
        {
            get
            {
                if (citizenship == null)
                    citizenship = new List<Country>();
                return citizenship;
            }
            set
            {
                RemoveAllCitizenship();
                if (value != null)
                {
                    foreach (Country oCountry in value)
                        AddCitizenship(oCountry);
                }
            }
        }

        public void AddCitizenship(Country newCountry)
        {
            if (newCountry == null)
                return;
            if (citizenship == null)
                citizenship = new List<Country>();
            if (!citizenship.Contains(newCountry))
                citizenship.Add(newCountry);
        }

        public void RemoveCitizenship(Country oldCountry)
        {
            if (oldCountry == null)
                return;
            if (citizenship != null)
                if (citizenship.Contains(oldCountry))
                    citizenship.Remove(oldCountry);
        }

        public void RemoveAllCitizenship()
        {
            if (citizenship != null)
                citizenship.Clear();
        }

    }
}