// File:    EquipmentTypeService.cs
// Author:  Lana
// Created: 28 May 2020 11:59:51
// Purpose: Definition of Class EquipmentTypeService

using System.Collections.Generic;
using HealthcareBase.Dto;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;
using HealthcareBase.Service.HospitalResourcesService.EquipmentService.Interface;

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly RepositoryWrapper<IEquipmentTypeRepository> equipmentTypeRepository;

        public EquipmentTypeService(IEquipmentTypeRepository equipmentTypeRepository)
        {
            this.equipmentTypeRepository = new RepositoryWrapper<IEquipmentTypeRepository>(equipmentTypeRepository);
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
            DeleteFromHospitalizationTypes(equipmentType);
            DeleteFromProcedureTypes(equipmentType);
            equipmentTypeRepository.Repository.Delete(equipmentType);
        }

        private void DeleteFromHospitalizationTypes(EquipmentType equipmentType)
        {
            
        }

        private void DeleteFromProcedureTypes(EquipmentType equipmentType)
        {
        }

        public IEnumerable<EquipmentTypeDto> GetAllEquipmentTypes()
        {
            return equipmentTypeRepository.Repository.GetColumnsForMatching(
                condition: equipmentType => equipmentType.Id != -1,
                selection: equipmentType => new EquipmentTypeDto()
                {
                    Id = equipmentType.Id,
                    Name = equipmentType.Name,
                }
            );
        }
    }
}