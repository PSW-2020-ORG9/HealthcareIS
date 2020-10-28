// File:    HospitalizationService.cs
// Author:  Lana
// Created: 28 May 2020 12:20:01
// Purpose: Definition of Class HospitalizationService

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Notifications;
using Model.Schedule.Hospitalizations;
using Model.Users.Patient;
using Model.Utilities;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Service.ScheduleService.Validators;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.HospitalizationService
{
    public class HospitalizationService
    {
        private HospitalizationRepository hospitalizationRepository;
        private HospitalizationValidator hospitalizationValidator;
        private HospitalizationScheduleComplianceValidator scheduleValidator;
        private NotificationService.NotificationService notificationService;
        private TimeSpan timeLimit;

        public HospitalizationScheduleComplianceValidator ScheduleValidator { get => scheduleValidator; set => scheduleValidator = value; }

        public HospitalizationService(HospitalizationRepository hospitalizationRepository,
            HospitalizationValidator hospitalizationValidator,
            NotificationService.NotificationService notificationService, TimeSpan timeLimit)
        {
            this.hospitalizationRepository = hospitalizationRepository;
            this.hospitalizationValidator = hospitalizationValidator;
            this.notificationService = notificationService;
            this.timeLimit = timeLimit;
        }

        public IEnumerable<Hospitalization> GetByDate(DateTime date)
        {
            DateTime realDate = date.Date;
            return hospitalizationRepository.GetMatching(hospitalization => hospitalization.TimeInterval.Start.Date <= date
                                                                         && hospitalization.TimeInterval.End.Date >= date);
        }

        public IEnumerable<Hospitalization> GetAll()
        {
            return hospitalizationRepository.GetAll();
        }

        public IEnumerable<Hospitalization> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            return hospitalizationRepository.GetByPatientAndTime(patient, time);
        }

        public IEnumerable<Hospitalization> GetByEquipmentInUseAndTime(IEnumerable<EquipmentUnit> equipment, TimeInterval time)
        {
            return hospitalizationRepository.GetByEquipmentInUseAndTime(equipment, time);
        }

        public IEnumerable<Hospitalization> GetUpcomingByPatient(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            return hospitalizationRepository.GetMatching(hosp => hosp.Patient.Equals(patient) &&
                                                          hosp.TimeInterval.Start.Date >= DateTime.Now);
        }

        public IEnumerable<Hospitalization> GetByRoomAndTime(Room room, TimeInterval time)
        {
            return hospitalizationRepository.GetByRoomAndTime(room, time);
        }

        public Hospitalization GetByID(int id)
        {
            return hospitalizationRepository.GetByID(id);
        }

        public Hospitalization Schedule(Hospitalization hospitalization)
        {
            if (hospitalization is null)
                throw new BadRequestException();
            ValidateForScheduling(hospitalization);
            Hospitalization createdHospitalization = hospitalizationRepository.Create(hospitalization);
            notificationService.Notify(HospitalizationUpdateType.Scheduled, createdHospitalization);
            return createdHospitalization;
        }

        public Hospitalization Reschedule(Hospitalization hospitalization)
        {
            if (hospitalization is null)
                throw new BadRequestException();
            ValidateForRescheduling(hospitalization);
            Hospitalization updatedHospitalization = hospitalizationRepository.Update(hospitalization);
            notificationService.Notify(HospitalizationUpdateType.Rescheduled, updatedHospitalization);
            return updatedHospitalization;
        }

        public void Cancel(Hospitalization hospitalization)
        {
            if (hospitalization is null)
                throw new BadRequestException();
            ValidateForCancelling(hospitalization);
            hospitalizationRepository.Delete(hospitalization);
            notificationService.Notify(HospitalizationUpdateType.Cancelled, hospitalization);
        }

        private void ValidateForScheduling(Hospitalization hospitalization)
        {
            ValidateAdmissionTimeLimit(hospitalization);
            hospitalizationValidator.ValidateHospitalization(hospitalization);
            scheduleValidator.ValidateComplianceForScheduling(hospitalization);
            ValidateAdmissionTimeLimit(hospitalization);
        }

        private void ValidateForRescheduling(Hospitalization hospitalization)
        {
            Hospitalization oldHospitalization = hospitalizationRepository.GetByID(hospitalization.GetKey());
            hospitalizationValidator.ValidateHospitalization(hospitalization);
            if (!hospitalization.Patient.Equals(oldHospitalization.Patient))
                throw new BadRequestException();
            if (hospitalization.TimeInterval.Start.Equals(oldHospitalization.TimeInterval.Start))
                ValidateForDischargeRescheduling(hospitalization, oldHospitalization);
            else
                ValidateForAdmissionRescheduling(hospitalization, oldHospitalization);
        }

        private void ValidateForAdmissionRescheduling(Hospitalization newHospitalization, Hospitalization oldHospitalization)
        {
            ValidateAdmissionTimeLimit(newHospitalization);
            ValidateAdmissionTimeLimit(oldHospitalization);
            scheduleValidator.ValidateComplianceForRescheduling(newHospitalization);
            ValidateAdmissionTimeLimit(newHospitalization);
            ValidateAdmissionTimeLimit(oldHospitalization);
        }

        private void ValidateForDischargeRescheduling(Hospitalization newHospitalization, Hospitalization oldHospitalization)
        {
            ValidateDischargeTimeLimit(oldHospitalization);
            ValidateDischargeTimeLimit(newHospitalization);
            scheduleValidator.ValidateComplianceForRescheduling(newHospitalization);
            ValidateDischargeTimeLimit(oldHospitalization);
            ValidateDischargeTimeLimit(newHospitalization);
        }

        private void ValidateForCancelling(Hospitalization hospitalization)
        {
            ValidateAdmissionTimeLimit(hospitalization);
        }

        private void ValidateAdmissionTimeLimit(Hospitalization hospitalization)
        {
            if (hospitalization.TimeInterval.Start <= DateTime.Now.Date + timeLimit)
                throw new TimingException();
        }

        private void ValidateDischargeTimeLimit(Hospitalization hospitalization)
        {
            if (hospitalization.TimeInterval.End < DateTime.Now.Date)
                throw new TimingException();
        }
    }
}