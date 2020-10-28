// File:    HospitalizationResourcesDTO.cs
// Author:  Lana
// Created: 02 June 2020 10:19:22
// Purpose: Definition of Class HospitalizationResourcesDTO

using System.Collections.Generic;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Users.Patient;
using Model.Utilities;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationResourcesDTO
    {
        public HospitalizationType Type { get; set; }

        public Patient Patient { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

        public TimeIntervalCollection Timing { get; set; }
    }
}