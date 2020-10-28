// File:    Employment.cs
// Author:  Gudli
// Created: 20 April 2020 20:48:53
// Purpose: Definition of Class Employment

using System;

namespace Model.Users.Patient.EmploymentHistory
{
    public class Employment
    {
        private DateTime start;
        private DateTime end;
        private Job job;

        public Employment(DateTime start, DateTime end, Job job)
        {
            this.start = start;
            this.end = end;
            this.job = job;
        }

        public Employment()
        {
            job = new Job();
        }

        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public Job Job { get => job; set => job = value; }
    }
}