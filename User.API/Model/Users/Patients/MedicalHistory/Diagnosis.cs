// File:    Diagnosis.cs
// Author:  Lana
// Created: 20 April 2020 23:15:25
// Purpose: Definition of Class Diagnosis

using User.API.Infrastructure;

namespace User.API.Model.Users.Patients.MedicalHistory
{
    public class Diagnosis : Entity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInjury { get; set; }
    }
}