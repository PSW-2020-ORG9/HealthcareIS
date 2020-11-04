// File:    HospitalizationTypeService.cs
// Author:  Lana
// Created: 28 May 2020 12:20:01
// Purpose: Definition of Class HospitalizationTypeService

using System.Collections.Generic;
using Model.Schedule.Hospitalizations;
using Repository.Generics;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.ScheduleService.HospitalizationService
{
    public class HospitalizationTypeService
    {
        private readonly RepositoryWrapper<HospitalizationTypeRepository> hospitalizationTypeRepository;

        public HospitalizationTypeService(RepositoryWrapper<HospitalizationTypeRepository> hospitalizationTypeRepository)
        {
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
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