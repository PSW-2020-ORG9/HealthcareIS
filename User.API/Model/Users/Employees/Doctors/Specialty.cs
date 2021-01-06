// File:    Specialty.cs
// Author:  Lana
// Created: 13 April 2020 18:40:05
// Purpose: Definition of Class Specialty


using General;
using User.API.Infrastructure;

namespace User.API.Model.Users.Employees.Doctors
{
    public class Specialty : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}