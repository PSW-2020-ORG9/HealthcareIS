// File:    HospitalizationSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class HospitalizationSchedulingPreference

using Model.HospitalResources;
using Model.Utilities;

namespace Model.Schedule.SchedulingPreferences
{
    public class HospitalizationSchedulingPreference
    {
        public Room PreferredRoom { get; set; }

        public TimeInterval PreferredAdmissionDate { get; set; }

        public int Duration { get; set; }
    }
}