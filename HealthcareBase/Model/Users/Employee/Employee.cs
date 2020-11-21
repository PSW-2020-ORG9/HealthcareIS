// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

using Model.Users.Generalities;
using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Employee
{
    public class Employee : Entity<int>
    {
        [Key]
        public int EmployeeID { get; set; }
        
        [ForeignKey("Person")]
        public string PersonJmbg { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public EmployeeStatus Status { get; set; }

        public int GetKey() => EmployeeID;
        public void SetKey(int id) => EmployeeID = id;
    }
}