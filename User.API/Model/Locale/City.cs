// File:    City.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class City

using User.API.Infrastructure;

namespace User.API.Model.Locale
{
    public class City : Entity<int>
    {
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}