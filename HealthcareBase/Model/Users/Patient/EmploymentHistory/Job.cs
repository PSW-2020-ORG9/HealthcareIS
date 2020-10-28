// File:    Job.cs
// Author:  Gudli
// Created: 20 April 2020 17:39:56
// Purpose: Definition of Class Job

using System.Collections.Generic;

namespace Model.Users.Patient.EmploymentHistory
{
    public class Job
    {
        private List<WorkplaceHazard> hazards;

        public string Field { get; set; }

        public string Role { get; set; }

        public string CompanyName { get; set; }

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
                    foreach (var oWorkplaceHazard in value)
                        AddHazards(oWorkplaceHazard);
            }
        }

        public void AddHazards(WorkplaceHazard newWorkplaceHazard)
        {
            if (newWorkplaceHazard == null)
                return;
            if (hazards == null)
                hazards = new List<WorkplaceHazard>();
            if (!hazards.Contains(newWorkplaceHazard))
                hazards.Add(newWorkplaceHazard);
        }

        public void RemoveHazards(WorkplaceHazard oldWorkplaceHazard)
        {
            if (oldWorkplaceHazard == null)
                return;
            if (hazards != null)
                if (hazards.Contains(oldWorkplaceHazard))
                    hazards.Remove(oldWorkplaceHazard);
        }

        public void RemoveAllHazards()
        {
            if (hazards != null)
                hazards.Clear();
        }
    }
}