// File:    EquipmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:57:59
// Purpose: Definition of Class EquipmentService

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;

namespace HealthcareBase.Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentService
    {
        private readonly RepositoryWrapper<EquipmentTypeRepository> equipmentTypeRepository;
        private readonly RepositoryWrapper<EquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<HospitalizationRepository> hospitalizationRepository;

        public EquipmentService(
            EquipmentUnitRepository equipmentUnitRepository,
            EquipmentTypeRepository equipmentTypeRepository,
            HospitalizationRepository hospitalizationRepository)
        {
            this.equipmentUnitRepository = new RepositoryWrapper<EquipmentUnitRepository>(equipmentUnitRepository);
            this.equipmentTypeRepository = new RepositoryWrapper<EquipmentTypeRepository>(equipmentTypeRepository);
            this.hospitalizationRepository =
                new RepositoryWrapper<HospitalizationRepository>(hospitalizationRepository);
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