// File:    EquipmentTypeService.cs
// Author:  Lana
// Created: 28 May 2020 11:59:51
// Purpose: Definition of Class EquipmentTypeService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Repository.ScheduleRepository.ProceduresRepository;

namespace Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentTypeService
    {
        private readonly EquipmentService equipmentService;
        private readonly EquipmentTypeRepository equipmentTypeRepository;
        private readonly HospitalizationTypeRepository hospitalizationTypeRepository;
        private readonly ProcedureTypeRepository procedureTypeRepository;

        public EquipmentTypeService(EquipmentTypeRepository equipmentTypeRepository,
            HospitalizationTypeRepository hospitalizationTypeRepository,
            ProcedureTypeRepository procedureTypeRepository,
            EquipmentService equipmentService)
        {
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
            this.procedureTypeRepository = procedureTypeRepository;
            this.equipmentService = equipmentService;
        }

        public EquipmentType GetByID(int id)
        {
            return equipmentTypeRepository.GetByID(id);
        }

        public IEnumerable<EquipmentType> GetAll()
        {
            return equipmentTypeRepository.GetAll();
        }

        public EquipmentType Create(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new BadRequestException();
            return equipmentTypeRepository.Create(equipmentType);
        }

        public EquipmentType Update(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new BadRequestException();
            return equipmentTypeRepository.Update(equipmentType);
        }

        public void Delete(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new BadRequestException();
            equipmentService.DeleteByType(equipmentType);
            DeleteFromHospitalizationTypes(equipmentType);
            DeleteFromProcedureTypes(equipmentType);
            equipmentTypeRepository.Delete(equipmentType);
        }

        private void DeleteFromHospitalizationTypes(EquipmentType equipmentType)
        {
            foreach (var hospitalizationType in hospitalizationTypeRepository.GetAll())
                if (hospitalizationType.NecessaryEquipment.Contains(equipmentType))
                {
                    hospitalizationType.RemoveNecessaryEquipment(equipmentType);
                    hospitalizationTypeRepository.Update(hospitalizationType);
                }
        }

        private void DeleteFromProcedureTypes(EquipmentType equipmentType)
        {
            foreach (var procedureType in procedureTypeRepository.GetAll())
                if (procedureType.NecessaryEquipment.Contains(equipmentType))
                {
                    procedureType.RemoveNecessaryEquipment(equipmentType);
                    procedureTypeRepository.Update(procedureType);
                }
        }
    }
}