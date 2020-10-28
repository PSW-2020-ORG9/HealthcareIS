// File:    HospitalizationSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class HospitalizationSchedulingPreference

using Model.HospitalResources;
using Model.Utilities;
using System;

namespace Model.Schedule.SchedulingPreferences
{
    public class HospitalizationSchedulingPreference
    {
        private Room preferredRoom;
        private TimeInterval preferredAdmissionDate;
        private int duration;

        public Room PreferredRoom { get => preferredRoom; set => preferredRoom = value; }
        public TimeInterval PreferredAdmissionDate { get => preferredAdmissionDate; set => preferredAdmissionDate = value; }
        public int Duration { get => duration; set => duration = value; }
    }
}