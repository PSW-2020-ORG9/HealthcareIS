// File:    HospitalizationScheduleFittingService.cs
// Author:  Lana
// Created: 02 June 2020 08:53:31
// Purpose: Definition of Class HospitalizationScheduleFittingService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Service.ScheduleService.AvailabilityCalculators;

namespace HealthcareBase.Service.ScheduleService.ScheduleFittingService
{
    public class HospitalizationScheduleFittingService
    {
        private readonly CurrentScheduleContext context;
        private readonly PreferenceToResourceConverter preferenceToResourceConverter;

        public HospitalizationScheduleFittingService(
            CurrentScheduleContext context,
            PreferenceToResourceConverter preferenceToResourceConverter)
        {
            this.context = context;
            this.preferenceToResourceConverter = preferenceToResourceConverter;
        }

        public IEnumerable<Hospitalization> FitForScheduling(HospitalizationPreferenceDTO preference)
        {
            var resources =
                preferenceToResourceConverter.ConvertHospitalizationPreference(preference);

            var patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(null));
            var roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(null));

            return GenerateHospitalizations(resources, patientAvailability, roomAvailabilities);
        }

        public IEnumerable<Hospitalization> FitForRescheduling(
            HospitalizationPreferenceDTO preference, Hospitalization hospitalization)
        {
            var resources =
                preferenceToResourceConverter.ConvertHospitalizationPreference(preference);

            var patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(hospitalization));
            var roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(hospitalization));

            return GenerateHospitalizations(resources, patientAvailability, roomAvailabilities);
        }

        private IEnumerable<RoomAvailabilityDTO> CalculateRoomAvailabilites(
            HospitalizationResourcesDTO resources, RoomAvailabilityCalculator calculator)
        {
            var roomAvailabilities = new List<RoomAvailabilityDTO>();
            foreach (var room in resources.Rooms)
            {
                var initialRoomAvailability = new RoomAvailabilityDTO
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
            var initialPatientAvailability = new PatientAvailabilityDTO
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
            var hospitalizations = new List<Hospitalization>();
            var duration = new TimeSpan(resources.Type.UsualNumberOfDays * 24, 0, 0);

            foreach (var room in rooms)
            {
                var matched = patient.Availability.Overlap(room.Availability);
                foreach (var slot in MakeSlots(matched.Intervals, duration))
                    hospitalizations.Add(new Hospitalization
                    {
                        HospitalizationType = resources.Type,
                        Patient = resources.Patient,
                        Room = room.Room
                    });
            }

            return hospitalizations;
        }

        private IEnumerable<TimeInterval> MakeSlots(IEnumerable<TimeInterval> availableTimes, TimeSpan duration)
        {
            var slots = new List<TimeInterval>();
            foreach (var available in availableTimes)
            {
                var slotStart = available.Start;
                var slotEnd = slotStart + duration;

                while (slotEnd <= available.End)
                {
                    slots.Add(new TimeInterval {Start = slotStart, End = slotEnd});
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