// File:    ProcedureScheduleFittingService.cs
// Author:  Lana
// Created: 02 June 2020 03:12:41
// Purpose: Definition of Class ProcedureScheduleFittingService

using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Utilities;
using Service.ScheduleService.AvailabilityCalculators;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.ScheduleFittingService
{
    public class ProcedureScheduleFittingService
    {
        private CurrentScheduleContext context;
        private PreferenceToResourceConverter preferenceToResourceConverter;

        public ProcedureScheduleFittingService(CurrentScheduleContext context, PreferenceToResourceConverter preferenceToResourceConverter)
        {
            this.context = context;
            this.preferenceToResourceConverter = preferenceToResourceConverter;
        }

        public IEnumerable<Procedure> FitForScheduling(ProcedurePreferenceDTO preference)
        {
            ProcedureResourcesDTO resources =
                preferenceToResourceConverter.ConvertProcedurePreference(preference);

            PatientAvailabilityDTO patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(null));
            IEnumerable<RoomAvailabilityDTO> roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(null));
            IEnumerable<DoctorAvailabilityDTO> doctorAvailabilities =
                CalculateDoctorAvailabilities(resources, MakeDoctorCalculator(null));

            return GenerateProcedures(resources, roomAvailabilities, patientAvailability, doctorAvailabilities);
        }

        public IEnumerable<Procedure> FitForRescheduling(ProcedurePreferenceDTO preference, Procedure procedure)
        {
            ProcedureResourcesDTO resources =
                preferenceToResourceConverter.ConvertProcedurePreference(preference);

            PatientAvailabilityDTO patientAvailability =
                CalculatePatientAvailability(resources, MakePatientCalculator(procedure));
            IEnumerable<RoomAvailabilityDTO> roomAvailabilities =
                CalculateRoomAvailabilites(resources, MakeRoomCalculator(procedure));
            IEnumerable<DoctorAvailabilityDTO> doctorAvailabilities =
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

        private IEnumerable<DoctorAvailabilityDTO> CalculateDoctorAvailabilities(ProcedureResourcesDTO resources, DoctorAvailabilityCalculator calculator)
        {
            List<DoctorAvailabilityDTO> doctorAvailabilities = new List<DoctorAvailabilityDTO>();
            foreach (Doctor doctor in resources.Doctors)
            {
                DoctorAvailabilityDTO initialDoctorAvailability = new DoctorAvailabilityDTO()
                {
                    Doctor = doctor,
                    Availability = new TimeIntervalCollection(resources.Timing.Intervals)
                };

                doctorAvailabilities.Add(calculator.Calculate(initialDoctorAvailability, context));
            }

            return doctorAvailabilities;
        }

        private IEnumerable<RoomAvailabilityDTO> CalculateRoomAvailabilites(ProcedureResourcesDTO resources, RoomAvailabilityCalculator calculator)
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

        private PatientAvailabilityDTO CalculatePatientAvailability(ProcedureResourcesDTO resources, PatientAvailabilityCalculator calculator)
        {
            PatientAvailabilityDTO initialPatientAvailability = new PatientAvailabilityDTO()
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
            List<Procedure> hospitalizations = new List<Procedure>();
            TimeSpan duration = resources.Type.Duration;

            foreach (RoomAvailabilityDTO room in rooms)
            {
                foreach (DoctorAvailabilityDTO doctor in doctors)
                {
                    TimeIntervalCollection matched =
                        patient.Availability.Overlap(room.Availability).Overlap(doctor.Availability);
                    foreach (TimeInterval slot in MakeSlots(matched.Intervals, duration))
                    {
                        Procedure toAdd = CreateProcedure(resources.Type);
                        toAdd.TimeInterval = slot;
                        toAdd.Patient = patient.Patient;
                        toAdd.Doctor = doctor.Doctor;
                        toAdd.Room = room.Room;
                    }
                }
            }

            return hospitalizations;
        }

        private Procedure CreateProcedure(ProcedureType type)
        {
            if (type.Kind.Equals(ProcedureKind.Examination))
                return new Examination();
            else
                return new Surgery();
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

    }
}