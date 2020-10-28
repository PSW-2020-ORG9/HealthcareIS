// File:    HospitalizationTypeService.cs
// Author:  Lana
// Created: 28 May 2020 12:20:01
// Purpose: Definition of Class HospitalizationTypeService

using System.Collections.Generic;
using Model.Schedule.Hospitalizations;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.ScheduleService.HospitalizationService
{
    public class HospitalizationTypeService
    {
        private readonly HospitalizationTypeRepository hospitalizationTypeRepository;

        public HospitalizationTypeService(HospitalizationTypeRepository hospitalizationTypeRepository)
        {
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
        }

        public IEnumerable<HospitalizationType> GetAll()
        {
            return hospitalizationTypeRepository.GetAll();
        }

        public HospitalizationType GetByID(int id)
        {
            return hospitalizationTypeRepository.GetByID(id);
        }
    }
}