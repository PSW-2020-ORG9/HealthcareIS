// File:    HospitalizationTypeService.cs
// Author:  Lana
// Created: 28 May 2020 12:20:01
// Purpose: Definition of Class HospitalizationTypeService

using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;

namespace HealthcareBase.Service.ScheduleService.HospitalizationService
{
    public class HospitalizationTypeService
    {
        private readonly RepositoryWrapper<HospitalizationTypeRepository> hospitalizationTypeRepository;

        public HospitalizationTypeService(HospitalizationTypeRepository hospitalizationTypeRepository)
        {
            this.hospitalizationTypeRepository =
                new RepositoryWrapper<HospitalizationTypeRepository>(hospitalizationTypeRepository);
        }

        public IEnumerable<HospitalizationType> GetAll()
        {
            return hospitalizationTypeRepository.Repository.GetAll();
        }

        public HospitalizationType GetByID(int id)
        {
            return hospitalizationTypeRepository.Repository.GetByID(id);
        }
    }
}