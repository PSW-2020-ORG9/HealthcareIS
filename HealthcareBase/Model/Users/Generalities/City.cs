// File:    City.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class City

using System;

namespace Model.Users.Generalities
{
    public class City : Repository.Generics.Entity<int>
    {
        private String name;
        private String postalCode;
        private Country country;
        private int id;

        public string Name { get => name; set => name = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public Country Country { get => country; set => country = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is City city &&
                   id == city.id;
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