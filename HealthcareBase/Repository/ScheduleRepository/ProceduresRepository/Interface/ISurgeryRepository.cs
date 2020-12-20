// File:    SurgeryRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface SurgeryRepository

using System;
using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface
{
    public interface ISurgeryRepository : IWrappableRepository<Surgery, int>
    {
        IEnumerable<Surgery> GetByDoctorAndTime(Doctor doctor, TimeInterval time);

        IEnumerable<Surgery> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates);

        IEnumerable<Surgery> GetByRoomAndTime(Room room, TimeInterval time);

        IEnumerable<Surgery> GetByPatientAndTime(Patient patient, TimeInterval time);

        IEnumerable<Surgery> GetByPatient(Patient patient);
    }
}