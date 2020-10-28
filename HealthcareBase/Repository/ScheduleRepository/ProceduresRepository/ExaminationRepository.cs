// File:    ExaminationRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface ExaminationRepository

using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public interface ExaminationRepository : Repository<Examination, int>
    {
        IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time);

        IEnumerable<Examination> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates);

        IEnumerable<Examination> GetByRoomAndTime(Room room, TimeInterval time);

        IEnumerable<Examination> GetByPatientAndTime(Patient patient, TimeInterval time);

        IEnumerable<Examination> GetByPatient(Patient patient);
    }
}