// File:    EmployeeAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:47
// Purpose: Definition of Class EmployeeAccount

using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.UserAccounts
{
    public class EmployeeAccount : UserAccount
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee.Employee Employee { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public EmployeeType EmployeeType { get; set; }

        public override bool Equals(object obj)
            => obj is EmployeeAccount account &&
                   Id == account.Id;
    }
}