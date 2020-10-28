// File:    WorkplaceHazard.cs
// Author:  Gudli
// Created: 20 April 2020 21:03:42
// Purpose: Definition of Class WorkplaceHazard

namespace Model.Users.Patient.EmploymentHistory
{
    public class WorkplaceHazard
    {
        public string Description { get; set; }

        public HazardType Type { get; set; }
    }
}