// File:    EmploymentInformation.cs
// Author:  Gudli
// Created: 20 April 2020 17:55:19
// Purpose: Definition of Class EmploymentInformation

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.EmploymentHistory
{
    [Owned]
    public class EmploymentInformation
    {
        private List<Employment> employmentHistory;

        [Column(TypeName = "nvarchar(24)")]
        public EmploymentStatus EmploymentStatus { get; set; }

        public IEnumerable<Employment> EmploymentHistory
        {
            get
            {
                if (employmentHistory == null)
                    employmentHistory = new List<Employment>();
                return employmentHistory;
            }
            set
            {
                RemoveAllEmployments();
                if (value != null)
                    foreach (var employment in value)
                        AddEmployment(employment);
            }
        }

        public void AddEmployment(Employment newEmployment)
        {
            if (newEmployment == null)
                return;
            if (employmentHistory == null)
                employmentHistory = new List<Employment>();
            if (!employmentHistory.Contains(newEmployment))
                employmentHistory.Add(newEmployment);
        }

        public void RemoveEmployment(Employment oldEmployment)
        {
            if (oldEmployment == null)
                return;
            if (employmentHistory != null)
                if (employmentHistory.Contains(oldEmployment))
                    employmentHistory.Remove(oldEmployment);
        }

        public void RemoveAllEmployments()
        {
            if (employmentHistory != null)
                employmentHistory.Clear();
        }
    }
}