// File:    Country.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Country

using User.API.Infrastructure;

namespace User.API.Model.Locale
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}