// File:    WorkplaceHazard.cs
// Author:  Gudli
// Created: 20 April 2020 21:03:42
// Purpose: Definition of Class WorkplaceHazard

using System;

namespace Model.Users.Patient.EmploymentHistory
{
    public class WorkplaceHazard
    {
        private String description;
        private HazardType type;

        public string Description { get => description; set => description = value; }
        public HazardType Type { get => type; set => type = value; }
    }
}