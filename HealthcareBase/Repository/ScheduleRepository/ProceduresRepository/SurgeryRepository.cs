// File:    SurgeryRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface SurgeryRepository

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
    public interface SurgeryRepository : Repository<Surgery, int>
    {
        IEnumerable<Surgery> GetByDoctorAndTime(Doctor doctor, TimeInterval time);

        IEnumerable<Surgery> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates);

        IEnumerable<Surgery> GetByRoomAndTime(Room room, TimeInterval time);

        IEnumerable<Surgery> GetByPatientAndTime(Patient patient, TimeInterval time);

        IEnumerable<Surgery> GetByPatient(Patient patient);
    }
}