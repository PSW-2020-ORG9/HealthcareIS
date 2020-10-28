// File:    SurgeryService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class SurgeryService

using Model.CustomExceptions;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Utilities;
using Repository.ScheduleRepository.ProceduresRepository;
using Service.ScheduleService.Validators;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.ProcedureService
{
    public class SurgeryService : AbstractProcedureSchedulingService<Surgery>
    {
        private SurgeryRepository surgeryRepository;

        public SurgeryService(SurgeryRepository surgeryRepository, NotificationService.NotificationService notificationService,
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator, TimeSpan timeLimit) :
            base(notificationService, scheduleValidator, procedureValidator, timeLimit)
        {
            this.surgeryRepository = surgeryRepository;
        }

        public override Surgery GetByID(int id)
        {
            return surgeryRepository.GetByID(id);
        }

        public IEnumerable<Surgery> GetAll()
        {
            return surgeryRepository.GetAll();
        }

        public IEnumerable<Surgery> GetByDate(DateTime date)
        {
            return surgeryRepository.GetMatching(surgery => surgery.TimeInterval.Start.Date.Equals(date.Date));
        }

        public IEnumerable<Surgery> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return surgeryRepository.GetByDoctorAndTime(doctor, time);
        }

        protected override Surgery Create(Surgery procedure)
        {
            return surgeryRepository.Create(procedure);
        }

        protected override Surgery Update(Surgery procedure)
        {
            return surgeryRepository.Update(procedure);
        }

        protected override void Delete(Surgery procedure)
        {
            surgeryRepository.Delete(procedure);
        }
    }
}