// File:    EquipmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:57:59
// Purpose: Definition of Class EquipmentService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentService
    {
        private readonly RepositoryWrapper<EquipmentTypeRepository> equipmentTypeRepository;
        private readonly RepositoryWrapper<EquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<HospitalizationRepository> hospitalizationRepository;

        public EquipmentService(RepositoryWrapper<EquipmentUnitRepository> equipmentUnitRepository,
            RepositoryWrapper<EquipmentTypeRepository> equipmentTypeRepository,
            RepositoryWrapper<HospitalizationRepository> hospitalizationRepository)
        {
            this.equipmentUnitRepository = equipmentUnitRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.hospitalizationRepository = hospitalizationRepository;
        }

        public EquipmentUnit GetByID(int id)
        {
            return equipmentUnitRepository.Repository.GetByID(id);
        }

        public IEnumerable<EquipmentUnit> GetAll()
        {
            return equipmentUnitRepository.Repository.GetAll();
        }

        public EquipmentUnit Create(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            if (!equipmentTypeRepository.Repository.ExistsByID(equipmentUnit.EquipmentType.GetKey()))
                equipmentUnit.EquipmentType = equipmentTypeRepository.Repository.Create(equipmentUnit.EquipmentType);
            return equipmentUnitRepository.Repository.Create(equipmentUnit);
        }

        public EquipmentUnit Update(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            return equipmentUnitRepository.Repository.Update(equipmentUnit);
        }

        public void Delete(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            DeleteFromHospitalizations(equipmentUnit);
            equipmentUnitRepository.Repository.Delete(equipmentUnit);
        }

        private void DeleteFromHospitalizations(EquipmentUnit equipmentUnit)
        {
            foreach (var hospitalization in hospitalizationRepository.Repository.GetAll())
                if (hospitalization.EquipmentInUse.Contains(equipmentUnit))
                {
                    hospitalization.RemoveEquipmentInUse(equipmentUnit);
                    hospitalizationRepository.Repository.Update(hospitalization);
                }
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
            equipmentType = equipmentTypeRepository.Repository.GetByID(equipmentType.GetKey());
            foreach (var equipmentUnit in equipmentUnitRepository.Repository.GetAll())
                if (equipmentUnit.EquipmentType.Equals(equipmentType))
                    Delete(equipmentUnit);
        }
    }
}