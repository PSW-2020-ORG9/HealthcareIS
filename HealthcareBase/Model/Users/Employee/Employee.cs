// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

using System;

namespace Model.Users.Employee
{
    public class Employee : Model.Users.Generalities.Person, Repository.Generics.Entity<int>
    {
        protected EmployeeStatus status;
        protected int employeeID;

        public EmployeeStatus Status { get => status; set => status = value; }

        public int EmployeeID { get => employeeID; set => employeeID = value; }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   employeeID == employee.employeeID;
        }

        public override int GetHashCode()
        {
            return 2070159828 + employeeID.GetHashCode();
        }

        public int GetKey()
        {
            return employeeID;
        }

        public void SetKey(int id)
        {
            employeeID = id;
        }
    }
}