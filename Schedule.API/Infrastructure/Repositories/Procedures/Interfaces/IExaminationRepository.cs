// File:    ExaminationRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface ExaminationRepository

using System;
using System.Collections.Generic;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Infrastructure.Repositories.Procedures.Interfaces
{
    public interface IExaminationRepository : IWrappableRepository<Examination, int>
    {
        IEnumerable<Examination> GetByDoctorAndDates(Doctor doctor, IEnumerable<DateTime> dates);
        IEnumerable<Examination> GetByPatientId(int patientId);
        IEnumerable<Examination> GetByDoctorAndDate(int doctorId, DateTime date);
        IEnumerable<Examination> GetByDoctorAndExaminationStart(int doctorId, DateTime date);
    }
}