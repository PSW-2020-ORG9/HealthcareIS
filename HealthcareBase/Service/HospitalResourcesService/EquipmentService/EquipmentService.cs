// File:    EquipmentService.cs
// Author:  Korisnik
// Created: 25 May 2020 12:57:59
// Purpose: Definition of Class EquipmentService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.HospitalResourcesService.EquipmentService
{
    public class EquipmentService
    {
        private readonly EquipmentTypeRepository equipmentTypeRepository;
        private readonly EquipmentUnitRepository equipmentUnitRepository;
        private readonly HospitalizationRepository hospitalizationRepository;

        public EquipmentService(EquipmentUnitRepository equipmentUnitRepository,
            EquipmentTypeRepository equipmentTypeRepository,
            HospitalizationRepository hospitalizationRepository)
        {
            this.equipmentUnitRepository = equipmentUnitRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.hospitalizationRepository = hospitalizationRepository;
        }

        public EquipmentUnit GetByID(int id)
        {
            return equipmentUnitRepository.GetByID(id);
        }

        public IEnumerable<EquipmentUnit> GetAll()
        {
            return equipmentUnitRepository.GetAll();
        }

        public EquipmentUnit Create(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            if (!equipmentTypeRepository.ExistsByID(equipmentUnit.EquipmentType.GetKey()))
                equipmentUnit.EquipmentType = equipmentTypeRepository.Create(equipmentUnit.EquipmentType);
            return equipmentUnitRepository.Create(equipmentUnit);
        }

        public EquipmentUnit Update(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            return equipmentUnitRepository.Update(equipmentUnit);
        }

        public void Delete(EquipmentUnit equipmentUnit)
        {
            if (equipmentUnit is null)
                throw new BadRequestException();
            DeleteFromHospitalizations(equipmentUnit);
            equipmentUnitRepository.Delete(equipmentUnit);
        }

        private void DeleteFromHospitalizations(EquipmentUnit equipmentUnit)
        {
            foreach (var hospitalization in hospitalizationRepository.GetAll())
                if (hospitalization.EquipmentInUse.Contains(equipmentUnit))
                {
                    hospitalization.RemoveEquipmentInUse(equipmentUnit);
                    hospitalizationRepository.Update(hospitalization);
                }
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
            equipmentType = equipmentTypeRepository.GetByID(equipmentType.GetKey());
            foreach (var equipmentUnit in equipmentUnitRepository.GetAll())
                if (equipmentUnit.EquipmentType.Equals(equipmentType))
                    Delete(equipmentUnit);
        }
    }
}