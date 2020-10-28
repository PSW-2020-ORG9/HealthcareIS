// File:    ProcedureScheduleFittingService.cs
// Author:  Lana
// Created: 02 June 2020 03:12:41
// Purpose: Definition of Class ProcedureScheduleFittingService

using System;
using System.Collections.Generic;
using Model.Schedule.Procedures;
using Model.Utilities;
using Service.ScheduleService.AvailabilityCalculators;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class ProcedureScheduleFittingService
    {
        private readonly CurrentScheduleContext context;
        private readonly PreferenceToResourceConverter preferenceToResourceConverter;

        public ProcedureScheduleFittingService(CurrentScheduleContext context,
            PreferenceToResourceConverter preferenceToResourceConverter)
        {
            this.context = context;
            this.preferenceToResourceConverter = preferenceToResourceConverter;
        }

        public IEnumerable<Procedure> FitForScheduling(ProcedurePreferenceDTO preference)
        {
            var resources =
                preferenceToResourceConverter.ConvertProcedurePreference(preference);

            var patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(null));
            var roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(null));
            var doctorAvailabilities =
                CalculateDoctorAvailabilities(resources, MakeDoctorCalculator(null));

            return GenerateProcedures(resources, roomAvailabilities, patientAvailability, doctorAvailabilities);
        }

        public IEnumerable<Procedure> FitForRescheduling(ProcedurePreferenceDTO preference, Procedure procedure)
        {
            var resources =
                preferenceToResourceConverter.ConvertProcedurePreference(preference);

            var patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(procedure));
            var roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(procedure));
            var doctorAvailabilities =
                CalculateDoctorAvailabilities(resources, MakeDoctorCalculator(procedure));

            return GenerateProcedures(resources, roomAvailabilities, patientAvailability, doctorAvailabilities);
        }

        private RoomAvailabilityCalculator MakeRoomCalculator(Procedure procedure)
        {
            return new ConsiderProceduresInRoomCalculator(
                procedure, new ConsiderRenovationsCalculator());
        }

        private PatientAvailabilityCalculator MakePatientCalculator(Procedure procedure)
        {
            return new ConsiderPatientsProceduresCalculator(
                procedure, new PatientAlwaysAvailableCalculator());
        }

        private DoctorAvailabilityCalculator MakeDoctorCalculator(Procedure procedure)
        {
            return new ConsiderDoctorsProceduresCalcuator(procedure,
                new ConsiderDoctorsShiftsCalculator());
        }

        private IEnumerable<DoctorAvailabilityDTO> CalculateDoctorAvailabilities(ProcedureResourcesDTO resources,
            DoctorAvailabilityCalculator calculator)
        {
            var doctorAvailabilities = new List<DoctorAvailabilityDTO>();
            foreach (var doctor in resources.Doctors)
            {
                var initialDoctorAvailability = new DoctorAvailabilityDTO
                {
                    Doctor = doctor,
                    Availability = new TimeIntervalCollection(resources.Timing.Intervals)
                };

                doctorAvailabilities.Add(calculator.Calculate(initialDoctorAvailability, context));
            }

            return doctorAvailabilities;
        }

        private IEnumerable<RoomAvailabilityDTO> CalculateRoomAvailabilites(ProcedureResourcesDTO resources,
            RoomAvailabilityCalculator calculator)
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

        private PatientAvailabilityDTO CalculatePatientAvailability(ProcedureResourcesDTO resources,
            PatientAvailabilityCalculator calculator)
        {
            var initialPatientAvailability = new PatientAvailabilityDTO
            {
                Patient = resources.Patient,
                Availability = new TimeIntervalCollection(resources.Timing.Intervals)
            };

            return calculator.Calculate(initialPatientAvailability, context);
        }

        private IEnumerable<Procedure> GenerateProcedures(
            ProcedureResourcesDTO resources, IEnumerable<RoomAvailabilityDTO> rooms,
            PatientAvailabilityDTO patient, IEnumerable<DoctorAvailabilityDTO> doctors)
        {
            var hospitalizations = new List<Procedure>();
            var duration = resources.Type.Duration;

            foreach (var room in rooms)
            foreach (var doctor in doctors)
            {
                var matched =
                    patient.Availability.Overlap(room.Availability).Overlap(doctor.Availability);
                foreach (var slot in MakeSlots(matched.Intervals, duration))
                {
                    var toAdd = CreateProcedure(resources.Type);
                    toAdd.TimeInterval = slot;
                    toAdd.Patient = patient.Patient;
                    toAdd.Doctor = doctor.Doctor;
                    toAdd.Room = room.Room;
                }
            }

            return hospitalizations;
        }

        private Procedure CreateProcedure(ProcedureType type)
        {
            if (type.Kind.Equals(ProcedureKind.Examination))
                return new Examination();
            return new Surgery();
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
    }
}