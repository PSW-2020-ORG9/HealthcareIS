// File:    Job.cs
// Author:  Gudli
// Created: 20 April 2020 17:39:56
// Purpose: Definition of Class Job

using System;
using System.Collections.Generic;

namespace Model.Users.Patient.EmploymentHistory
{
    public class Job
    {
        private String field;
        private String role;
        private String companyName;
        private List<WorkplaceHazard> hazards;

        public string Field { get => field; set => field = value; }
        public string Role { get => role; set => role = value; }
        public string CompanyName { get => companyName; set => companyName = value; }

        public IEnumerable<WorkplaceHazard> Hazards
        {
            get
            {
                if (hazards == null)
                    hazards = new List<WorkplaceHazard>();
                return hazards;
            }
            set
            {
                RemoveAllHazards();
                if (value != null)
                {
                    foreach (WorkplaceHazard oWorkplaceHazard in value)
                        AddHazards(oWorkplaceHazard);
                }
            }
        }

        public void AddHazards(WorkplaceHazard newWorkplaceHazard)
        {
            if (newWorkplaceHazard == null)
                return;
            if (this.hazards == null)
                this.hazards = new List<WorkplaceHazard>();
            if (!this.hazards.Contains(newWorkplaceHazard))
                this.hazards.Add(newWorkplaceHazard);
        }

        public void RemoveHazards(WorkplaceHazard oldWorkplaceHazard)
        {
            if (oldWorkplaceHazard == null)
                return;
            if (this.hazards != null)
                if (this.hazards.Contains(oldWorkplaceHazard))
                    this.hazards.Remove(oldWorkplaceHazard);
        }

        public void RemoveAllHazards()
        {
            if (hazards != null)
                hazards.Clear();
        }

    }
}