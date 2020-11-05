// File:    EquipmentTypeService.cs
// Author:  Lana
// Created: 28 May 2020 11:59:51
// Purpose: Definition of Class EquipmentTypeService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Repository.ScheduleRepository.ProceduresRepository;

namespace Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentTypeService
    {
        private readonly EquipmentService equipmentService;
        private readonly RepositoryWrapper<EquipmentTypeRepository> equipmentTypeRepository;
        private readonly RepositoryWrapper<HospitalizationTypeRepository> hospitalizationTypeRepository;
        private readonly RepositoryWrapper<ProcedureTypeRepository> procedureTypeRepository;

        public EquipmentTypeService(
            EquipmentTypeRepository equipmentTypeRepository,
            HospitalizationTypeRepository hospitalizationTypeRepository,
            ProcedureTypeRepository procedureTypeRepository,
            EquipmentService equipmentService)
        {
            this.equipmentTypeRepository = new RepositoryWrapper<EquipmentTypeRepository>(equipmentTypeRepository);
            this.hospitalizationTypeRepository =
                new RepositoryWrapper<HospitalizationTypeRepository>(hospitalizationTypeRepository);
            this.procedureTypeRepository = new RepositoryWrapper<ProcedureTypeRepository>(procedureTypeRepository);
            this.equipmentService = equipmentService;
        }

        public EquipmentType GetByID(int id)
        {
            return equipmentTypeRepository.Repository.GetByID(id);
        }

        public IEnumerable<EquipmentType> GetAll()
        {
            return equipmentTypeRepository.Repository.GetAll();
        }

        public EquipmentType Create(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new BadRequestException();
            return equipmentTypeRepository.Repository.Create(equipmentType);
        }

        public EquipmentType Update(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new BadRequestException();
            return equipmentTypeRepository.Repository.Update(equipmentType);
        }

        public void Delete(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new BadRequestException();
            equipmentService.DeleteByType(equipmentType);
            DeleteFromHospitalizationTypes(equipmentType);
            DeleteFromProcedureTypes(equipmentType);
            equipmentTypeRepository.Repository.Delete(equipmentType);
        }

        private void DeleteFromHospitalizationTypes(EquipmentType equipmentType)
        {
            foreach (var hospitalizationType in hospitalizationTypeRepository.Repository.GetAll())
                if (hospitalizationType.NecessaryEquipment.Contains(equipmentType))
                {
                    hospitalizationType.RemoveNecessaryEquipment(equipmentType);
                    hospitalizationTypeRepository.Repository.Update(hospitalizationType);
                }
        }

        private void DeleteFromProcedureTypes(EquipmentType equipmentType)
        {
            foreach (var procedureType in procedureTypeRepository.Repository.GetAll())
                if (procedureType.NecessaryEquipment.Contains(equipmentType))
                {
                    procedureType.RemoveNecessaryEquipment(equipmentType);
                    procedureTypeRepository.Repository.Update(procedureType);
                }
        }
    }
}