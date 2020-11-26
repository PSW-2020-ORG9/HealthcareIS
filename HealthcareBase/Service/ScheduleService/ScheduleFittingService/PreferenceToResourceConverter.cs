// File:    PreferenceToResourceConverter.cs
// Author:  Lana
// Created: 02 June 2020 10:01:03
// Purpose: Definition of Class PreferenceToResourceConverter

using System;
using System.Collections.Generic;
using System.Linq;
using Model.HospitalResources;
using Model.Users.Employee;
using Model.Utilities;
using Service.HospitalResourcesService.RoomService;
using Service.UsersService.EmployeeService;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class PreferenceToResourceConverter
    {
        private readonly DoctorService doctorService;
        private readonly RoomService roomService;
        private readonly TimeSpan timeLimit;

        public PreferenceToResourceConverter(DoctorService doctorService, RoomService roomService, TimeSpan timeLimit)
        {
            this.doctorService = doctorService;
            this.roomService = roomService;
            this.timeLimit = timeLimit;
        }

        public ProcedureResourcesDTO ConvertProcedurePreference(ProcedurePreferenceDTO preference)
        {
            var qualifiedDoctors = new List<Doctor>();
            var appropriateRooms = new List<Room>();

            var resources = new ProcedureResourcesDTO
            {
                Patient = preference.Patient,
                Details = preference.Details,
                Doctors = ConvertDoctors(preference.Preference.PreferredDoctor, qualifiedDoctors),
                Rooms = ConvertRooms(preference.Preference.PreferredRoom, appropriateRooms),
                Timing = ConvertTiming(preference)
            };

            return resources;
        }

        public HospitalizationResourcesDTO ConvertHospitalizationPreference(HospitalizationPreferenceDTO preference)
        {
            var appropriateRooms = roomService.GetAppropriate(preference.Type);

            var resources = new HospitalizationResourcesDTO
            {
                Patient = preference.Patient,
                Type = preference.Type,
                Rooms = ConvertRooms(preference.Preference.PreferredRoom, appropriateRooms),
                Timing = ConvertTiming(preference)
            };

            return resources;
        }

        private TimeIntervalCollection ConvertTiming(ProcedurePreferenceDTO preference)
        {
            var earliestAllowed = DateTime.Now + timeLimit;
            var defaultTime = new TimeInterval
            {
                Start = earliestAllowed,
                End = earliestAllowed.AddDays(5)
            };

            if (preference.Preference.PreferredTime is null)
                return new TimeIntervalCollection(defaultTime);
            return preference.Preference.PreferredTime.RemoveEarlier(DateTime.Now + timeLimit);
        }

        private TimeIntervalCollection ConvertTiming(HospitalizationPreferenceDTO preference)
        {
            var duration = preference.Preference.Duration;
            if (duration <= 0) duration = preference.Type.UsualNumberOfDays;
            var earliestAllowed = (DateTime.Now + timeLimit).Date;
            var defaultInterval = new TimeInterval
            {
                Start = earliestAllowed,
                End = earliestAllowed.AddDays(5 + duration)
            };

            if (preference.Preference.PreferredAdmissionDate is null)
                return new TimeIntervalCollection(defaultInterval);

            if (preference.Preference.PreferredAdmissionDate.End.Date < earliestAllowed)
                return new TimeIntervalCollection(defaultInterval);

            var time = new TimeInterval
            {
                Start = preference.Preference.PreferredAdmissionDate.Start,
                End = preference.Preference.PreferredAdmissionDate.End.Date.AddDays(duration)
            };
            if (time.Start < earliestAllowed)
                time.Start = earliestAllowed;
            return new TimeIntervalCollection(time);
        }

        private IEnumerable<Doctor> ConvertDoctors(Doctor preferred, IEnumerable<Doctor> qualified)
        {
            if (preferred is null)
                return qualified;
            if (!qualified.Contains(preferred))
                return qualified;
            return new List<Doctor> {preferred};
        }

        private IEnumerable<Room> ConvertRooms(Room preferred, IEnumerable<Room> appropriate)
        {
            if (preferred is null)
                return appropriate;
            if (!appropriate.Contains(preferred))
                return appropriate;
            return new List<Room> {preferred};
        }
    }
}