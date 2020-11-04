// File:    WorkplaceHazard.cs
// Author:  Gudli
// Created: 20 April 2020 21:03:42
// Purpose: Definition of Class WorkplaceHazard

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.EmploymentHistory
{
    [Owned]
    public class WorkplaceHazard
    {
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public HazardType Type { get; set; }
    }
}