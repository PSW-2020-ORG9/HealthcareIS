// File:    PreferenceToResourceConverter.cs
// Author:  Lana
// Created: 02 June 2020 10:01:03
// Purpose: Definition of Class PreferenceToResourceConverter

using Model.HospitalResources;
using Model.Schedule.SchedulingPreferences;
using Model.Users.Employee;
using Model.Utilities;
using Service.HospitalResourcesService.RoomService;
using Service.UsersService.EmployeeService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class PreferenceToResourceConverter
    {
        private DoctorService doctorService;
        private RoomService roomService;
        private TimeSpan timeLimit;

        public PreferenceToResourceConverter(DoctorService doctorService, RoomService roomService, TimeSpan timeLimit)
        {
            this.doctorService = doctorService;
            this.roomService = roomService;
            this.timeLimit = timeLimit;
        }

        public ProcedureResourcesDTO ConvertProcedurePreference(ProcedurePreferenceDTO preference)
        {
            IEnumerable<Doctor> qualifiedDoctors = doctorService.GetQualified(preference.Type);
            IEnumerable<Room> appropriateRooms = roomService.GetAppropriate(preference.Type);

            ProcedureResourcesDTO resources = new ProcedureResourcesDTO()
            {
                Patient = preference.Patient,
                Type = preference.Type,
                Doctors = ConvertDoctors(preference.Preference.PreferredDoctor, qualifiedDoctors),
                Rooms = ConvertRooms(preference.Preference.PreferredRoom, appropriateRooms),
                Timing = ConvertTiming(preference)
            };

            return resources;
        }

        public HospitalizationResourcesDTO ConvertHospitalizationPreference(HospitalizationPreferenceDTO preference)
        {
            IEnumerable<Room> appropriateRooms = roomService.GetAppropriate(preference.Type);

            HospitalizationResourcesDTO resources = new HospitalizationResourcesDTO()
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
            DateTime earliestAllowed = DateTime.Now + timeLimit;
            TimeInterval defaultTime = new TimeInterval()
            {
                Start = earliestAllowed,
                End = earliestAllowed.AddDays(5)
            };

            if (preference.Preference.PreferredTime is null)
                return new TimeIntervalCollection(defaultTime);
            else return preference.Preference.PreferredTime.RemoveEarlier(DateTime.Now + timeLimit);
        }

        private TimeIntervalCollection ConvertTiming(HospitalizationPreferenceDTO preference)
        {
            int duration = preference.Preference.Duration;
            if (duration <= 0) duration = preference.Type.UsualNumberOfDays;
            DateTime earliestAllowed = (DateTime.Now + timeLimit).Date;
            TimeInterval defaultInterval = new TimeInterval()
            {
                Start = earliestAllowed,
                End = earliestAllowed.AddDays(5 + duration)
            };

            if (preference.Preference.PreferredAdmissionDate is null)
                return new TimeIntervalCollection(defaultInterval);
            else if (preference.Preference.PreferredAdmissionDate.End.Date < earliestAllowed)
                return new TimeIntervalCollection(defaultInterval);
            else
            {
                TimeInterval time = new TimeInterval()
                {
                    Start = preference.Preference.PreferredAdmissionDate.Start,
                    End = preference.Preference.PreferredAdmissionDate.End.Date.AddDays(duration)
                };
                if (time.Start < earliestAllowed)
                    time.Start = earliestAllowed;
                return new TimeIntervalCollection(time);
            }
        }

        private IEnumerable<Doctor> ConvertDoctors(Doctor preferred, IEnumerable<Doctor> qualified)
        {
            if (preferred is null)
                return qualified;
            else if (!qualified.Contains(preferred))
                return qualified;
            else
                return new List<Doctor>() { preferred };
        }

        private IEnumerable<Room> ConvertRooms(Room preferred, IEnumerable<Room> appropriate)
        {
            if (preferred is null)
                return appropriate;
            else if (!appropriate.Contains(preferred))
                return appropriate;
            else
                return new List<Room>() { preferred };
        }

    }
}