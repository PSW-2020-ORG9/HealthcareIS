// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

using Model.Users.Generalities;
using Repository.Generics;

namespace Model.Users.Employee
{
    public class Employee : Person, Entity<int>
    {
        protected int employeeID;
        protected EmployeeStatus status;

        public EmployeeStatus Status
        {
            get => status;
            set => status = value;
        }

        public int EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public int GetKey()
        {
            return employeeID;
        }

        public void SetKey(int id)
        {
            employeeID = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   employeeID == employee.employeeID;
        }

        public override int GetHashCode()
        {
            return 2070159828 + employeeID.GetHashCode();
        }
    }
}