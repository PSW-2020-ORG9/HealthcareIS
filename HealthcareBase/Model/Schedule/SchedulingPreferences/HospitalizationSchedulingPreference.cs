// File:    HospitalizationSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class HospitalizationSchedulingPreference

using Microsoft.EntityFrameworkCore;
using Model.HospitalResources;
using Model.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Schedule.SchedulingPreferences
{
    [Owned]
    public class HospitalizationSchedulingPreference
    {
        [ForeignKey("PreferredRoom")]
        public int PreferredRoomId { get; set; }
        public Room PreferredRoom { get; set; }

        public TimeInterval PreferredAdmissionDate { get; set; }

        public int Duration { get; set; }
    }
}