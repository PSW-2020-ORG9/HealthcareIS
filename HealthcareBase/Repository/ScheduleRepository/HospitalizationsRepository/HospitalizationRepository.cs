// File:    HospitalizationRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Interface HospitalizationRepository

using System.Collections.Generic;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;

namespace Repository.ScheduleRepository.HospitalizationsRepository
{
    public interface HospitalizationRepository : Repository<Hospitalization, int>
    {
        IEnumerable<Hospitalization> GetByPatientAndTime(Patient patient, TimeInterval time);

        IEnumerable<Hospitalization>
            GetByEquipmentInUseAndTime(IEnumerable<EquipmentUnit> equipment, TimeInterval time);

        IEnumerable<Hospitalization> GetByRoomAndTime(Room room, TimeInterval time);

        IEnumerable<Hospitalization> GetByPatient(Patient patient);
    }
}