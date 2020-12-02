// File:    SurgeryService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class SurgeryService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class SurgeryService : AbstractProcedureSchedulingService<Surgery>
    {
        private readonly RepositoryWrapper<ISurgeryRepository> surgeryRepository;

        public SurgeryService(
            ISurgeryRepository surgeryRepository,
            ProcedureScheduleComplianceValidator scheduleValidator,
            TimeSpan timeLimit
        ) : base(timeLimit)
        {
            this.surgeryRepository = new RepositoryWrapper<ISurgeryRepository>(surgeryRepository);
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

        protected override void Validate(Surgery procedure)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateProcedure(Surgery procedure)
        {
            throw new NotImplementedException();
        }
    }
}