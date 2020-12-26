// File:    Department.cs
// Author:  Lana
// Created: 18 April 2020 17:20:00
// Purpose: Definition of Class Department


using User.API.Infrastructure;

namespace User.API.Model.Users.Employees.Doctors
{
    public class Department : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}