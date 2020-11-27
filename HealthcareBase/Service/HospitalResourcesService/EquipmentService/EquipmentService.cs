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
        private readonly RepositoryWrapper<IEquipmentUnitRepository> equipmentUnitRepository;

        public EquipmentService(IEquipmentUnitRepository equipmentUnitRepository)
        {
            this.equipmentUnitRepository = new RepositoryWrapper<IEquipmentUnitRepository>(equipmentUnitRepository);
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
            return new EquipmentUnit();
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
        }

        public void DeleteByType(EquipmentType equipmentType)
        {
           
        }

        public IEnumerable<EquipmentUnit> GetEquipmentByRoomId(int roomId)
        {
            return equipmentUnitRepository.Repository.GetMatching(equipmentUnit => equipmentUnit.CurrentLocation.Id == roomId);
        }
    }
}