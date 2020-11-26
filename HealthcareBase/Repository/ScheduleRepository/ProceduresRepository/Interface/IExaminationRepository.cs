// File:    ExaminationRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface ExaminationRepository

using System;
using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface
{
    public interface IExaminationRepository : IWrappableRepository<Examination, int>
    {
        IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time);

        IEnumerable<Examination> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates);

        IEnumerable<Examination> GetByRoomAndTime(Room room, TimeInterval time);

        IEnumerable<Examination> GetByPatientAndTime(Patient patient, TimeInterval time);

        IEnumerable<Examination> GetByPatient(Patient patient);
    }
}