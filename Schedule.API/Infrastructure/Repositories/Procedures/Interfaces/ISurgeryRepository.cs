// File:    SurgeryRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface SurgeryRepository

using System;
using System.Collections.Generic;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Infrastructure.Repositories.Procedures.Interfaces
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