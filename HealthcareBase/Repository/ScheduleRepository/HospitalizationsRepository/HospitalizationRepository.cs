// File:    HospitalizationRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Interface HospitalizationRepository

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository
{
    public interface HospitalizationRepository : IWrappableRepository<Hospitalization, int>
    {
        IEnumerable<Hospitalization> GetByPatientAndTime(Patient patient, TimeInterval time);

        IEnumerable<Hospitalization>
            GetByEquipmentInUseAndTime(IEnumerable<EquipmentUnit> equipment, TimeInterval time);

        IEnumerable<Hospitalization> GetByRoomAndTime(Room room, TimeInterval time);

        IEnumerable<Hospitalization> GetByPatient(Patient patient);
    }
}