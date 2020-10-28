// File:    ProcedureSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class ProcedureSchedulingPreference

using Model.HospitalResources;
using Model.Users.Employee;
using Model.Utilities;
using System;

namespace Model.Schedule.SchedulingPreferences
{
    public class ProcedureSchedulingPreference
    {
        private TimeIntervalCollection preferredTime;
        private Doctor preferredDoctor;
        private Room preferredRoom;

        public TimeIntervalCollection PreferredTime { get => preferredTime; set => preferredTime = value; }
        public Doctor PreferredDoctor { get => preferredDoctor; set => preferredDoctor = value; }
        public Room PreferredRoom { get => preferredRoom; set => preferredRoom = value; }
    }
}