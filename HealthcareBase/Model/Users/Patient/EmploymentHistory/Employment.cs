// File:    Employment.cs
// Author:  Gudli
// Created: 20 April 2020 20:48:53
// Purpose: Definition of Class Employment

using Microsoft.EntityFrameworkCore;
using System;

namespace Model.Users.Patient.EmploymentHistory
{
    [Owned]
    public class Employment
    {
        public Employment(DateTime start, DateTime end, Job job)
        {
            Start = start;
            End = end;
            Job = job;
        }

        public Employment()
        {
            Job = new Job();
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Job Job { get; set; }
    }
}