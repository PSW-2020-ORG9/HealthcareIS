// File:    Doctor.cs
// Author:  Lana
// Created: 13 April 2020 18:32:18
// Purpose: Definition of Class Doctor

using Model.HospitalResources;
using System;
using System.Collections.Generic;

namespace Model.Users.Employee
{
    public class Doctor : Employee
    {
        private Model.HospitalResources.Department department;
        private List<Specialty> specialties;

        public Department Department { get => department; set => department = value; }

        public IEnumerable<Specialty> Specialties
        {
            get
            {
                if (specialties == null)
                    specialties = new List<Specialty>();
                return specialties;
            }
            set
            {
                RemoveAllSpecialties();
                if (value != null)
                {
                    foreach (Specialty oSpecialty in value)
                        AddSpecialties(oSpecialty);
                }
            }
        }

        public void AddSpecialties(Specialty newSpecialty)
        {
            if (newSpecialty == null)
                return;
            if (specialties == null)
                specialties = new List<Specialty>();
            if (!specialties.Contains(newSpecialty))
                specialties.Add(newSpecialty);
        }

        public void RemoveSpecialties(Specialty oldSpecialty)
        {
            if (oldSpecialty == null)
                return;
            if (specialties != null)
                if (specialties.Contains(oldSpecialty))
                    specialties.Remove(oldSpecialty);
        }

        public void RemoveAllSpecialties()
        {
            if (specialties != null)
                specialties.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is Doctor employee &&
                   employeeID == employee.employeeID;
        }

        public override int GetHashCode()
        {
            return 2070159828 + employeeID.GetHashCode();
        }

    }
}