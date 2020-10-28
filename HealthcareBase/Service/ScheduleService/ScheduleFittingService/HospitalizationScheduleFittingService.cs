// File:    HospitalizationScheduleFittingService.cs
// Author:  Lana
// Created: 02 June 2020 08:53:31
// Purpose: Definition of Class HospitalizationScheduleFittingService

using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Utilities;
using Service.ScheduleService.AvailabilityCalculators;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationScheduleFittingService
    {
        private CurrentScheduleContext context;
        private PreferenceToResourceConverter preferenceToResourceConverter;

        public HospitalizationScheduleFittingService(CurrentScheduleContext context, PreferenceToResourceConverter preferenceToResourceConverter)
        {
            this.context = context;
            this.preferenceToResourceConverter = preferenceToResourceConverter;
        }

        public IEnumerable<Hospitalization> FitForScheduling(HospitalizationPreferenceDTO preference)
        {
            HospitalizationResourcesDTO resources =
                preferenceToResourceConverter.ConvertHospitalizationPreference(preference);

            PatientAvailabilityDTO patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(null));
            IEnumerable<RoomAvailabilityDTO> roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(null));

            return GenerateHospitalizations(resources, patientAvailability, roomAvailabilities);
        }

        public IEnumerable<Hospitalization> FitForRescheduling(
            HospitalizationPreferenceDTO preference, Hospitalization hospitalization)
        {
            HospitalizationResourcesDTO resources =
                preferenceToResourceConverter.ConvertHospitalizationPreference(preference);

            PatientAvailabilityDTO patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(hospitalization));
            IEnumerable<RoomAvailabilityDTO> roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(hospitalization));

            return GenerateHospitalizations(resources, patientAvailability, roomAvailabilities);
        }

        private IEnumerable<RoomAvailabilityDTO> CalculateRoomAvailabilites(
            HospitalizationResourcesDTO resources, RoomAvailabilityCalculator calculator)
        {
            List<RoomAvailabilityDTO> roomAvailabilities = new List<RoomAvailabilityDTO>();
            foreach (Room room in resources.Rooms)
            {
                RoomAvailabilityDTO initialRoomAvailability = new RoomAvailabilityDTO()
                {
                    Room = room,
                    Availability = new TimeIntervalCollection(resources.Timing.Intervals)
                };

                roomAvailabilities.Add(calculator.Calculate(initialRoomAvailability, context));
            }
            return roomAvailabilities;
        }

        private PatientAvailabilityDTO CalculatePatientAvailability(
            HospitalizationResourcesDTO resources, PatientAvailabilityCalculator calculator)
        {
            PatientAvailabilityDTO initialPatientAvailability = new PatientAvailabilityDTO()
            {
                Patient = resources.Patient,
                Availability = new TimeIntervalCollection(resources.Timing.Intervals)
            };

            return calculator.Calculate(initialPatientAvailability, context);
        }

        private IEnumerable<Hospitalization> GenerateHospitalizations(
            HospitalizationResourcesDTO resources, PatientAvailabilityDTO patient,
            IEnumerable<RoomAvailabilityDTO> rooms)
        {
            List<Hospitalization> hospitalizations = new List<Hospitalization>();
            TimeSpan duration = new TimeSpan(resources.Type.UsualNumberOfDays * 24, 0, 0);

            foreach (RoomAvailabilityDTO room in rooms)
            {
                TimeIntervalCollection matched = patient.Availability.Overlap(room.Availability);
                foreach (TimeInterval slot in MakeSlots(matched.Intervals, duration))
                {
                    hospitalizations.Add(new Hospitalization()
                    {
                        HospitalizationType = resources.Type,
                        Patient = resources.Patient,
                        Room = room.Room
                    });
                }
            }

            return hospitalizations;
        }

        private IEnumerable<TimeInterval> MakeSlots(IEnumerable<TimeInterval> availableTimes, TimeSpan duration)
        {
            List<TimeInterval> slots = new List<TimeInterval>();
            foreach (TimeInterval available in availableTimes)
            {
                DateTime slotStart = available.Start;
                DateTime slotEnd = slotStart + duration;

                while (slotEnd <= available.End)
                {
                    slots.Add(new TimeInterval() { Start = slotStart, End = slotEnd });
                    slotStart = slotEnd;
                    slotEnd = slotStart + duration;
                }
            }
            return slots;
        }

        private RoomAvailabilityCalculator MakeRoomCalculator(Hospitalization hospitalization)
        {
            return new ConsiderHospitalizationsInRoomCalculator(
                hospitalization, new ConsiderRenovationsCalculator());
        }

        private PatientAvailabilityCalculator MakePatientCalculator(Hospitalization hospitalization)
        {
            return new ConsiderPatientsHospitalizationsCalculator(
                hospitalization, new PatientAlwaysAvailableCalculator());
        }

    }
}