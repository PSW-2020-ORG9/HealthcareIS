// File:    SurgeryService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class SurgeryService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class SurgeryService : AbstractProcedureSchedulingService<Surgery>
    {
        private readonly RepositoryWrapper<SurgeryRepository> surgeryRepository;

        public SurgeryService(
            SurgeryRepository surgeryRepository,
            NotificationService.NotificationService notificationService,
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit
        ) : base(notificationService, scheduleValidator, procedureValidator, timeLimit)
        {
            this.surgeryRepository = new RepositoryWrapper<SurgeryRepository>(surgeryRepository);
        }

        public override Surgery GetByID(int id)
        {
            return surgeryRepository.Repository.GetByID(id);
        }

        public IEnumerable<Surgery> GetAll()
        {
            return surgeryRepository.Repository.GetAll();
        }

        public IEnumerable<Surgery> GetByDate(DateTime date)
        {
            return surgeryRepository.Repository.GetMatching(
                surgery => surgery.TimeInterval.Start.Date.Equals(date.Date));
        }

        public IEnumerable<Surgery> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return surgeryRepository.Repository.GetByDoctorAndTime(doctor, time);
        }

        protected override Surgery Create(Surgery procedure)
        {
            return surgeryRepository.Repository.Create(procedure);
        }

        protected override Surgery Update(Surgery procedure)
        {
            return surgeryRepository.Repository.Update(procedure);
        }

        protected override void Delete(Surgery procedure)
        {
            surgeryRepository.Repository.Delete(procedure);
        }
    }
}