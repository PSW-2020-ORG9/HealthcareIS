// File:    EmployeeAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:47
// Purpose: Definition of Class EmployeeAccount

using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.UserAccounts
{
    public class EmployeeAccount : UserAccount
    {
        protected Employee.Employee employee;
        protected EmployeeType employeeType;

        public Employee.Employee Employee
        {
            get => employee;
            set => employee = value;
        }

        [Column(TypeName = "nvarchar(24)")]
        public EmployeeType EmployeeType
        {
            get => employeeType;
            set => employeeType = value;
        }

        public override bool Equals(object obj)
        {
            return obj is EmployeeAccount account &&
                   id == account.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}