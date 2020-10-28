// File:    ProcedureSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class ProcedureSchedulingPreference

using Model.HospitalResources;
using Model.Users.Employee;
using Model.Utilities;

namespace Model.Schedule.SchedulingPreferences
{
    public class ProcedureSchedulingPreference
    {
        public TimeIntervalCollection PreferredTime { get; set; }

        public Doctor PreferredDoctor { get; set; }

        public Room PreferredRoom { get; set; }
    }
}