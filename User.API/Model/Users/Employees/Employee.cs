// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

using General;
using User.API.Model.Generalities;

namespace User.API.Model.Users.Employees
{
    public abstract class Employee : Entity<int>
    {
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}