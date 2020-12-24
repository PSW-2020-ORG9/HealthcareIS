// File:    Country.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Country

using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Generalities
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}