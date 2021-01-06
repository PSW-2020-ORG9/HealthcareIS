using General.Repository;
using Hospital.API.DTOs;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Model.Resources;
using System;
using System.Collections.Generic;

namespace Hospital.API.Services.Resources
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
                throw new ArgumentException();
            return equipmentTypeRepository.Repository.Create(equipmentType);
        }

        public EquipmentType Update(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new ArgumentException();
            return equipmentTypeRepository.Repository.Update(equipmentType);
        }

        public void Delete(EquipmentType equipmentType)
        {
            if (equipmentType is null)
                throw new ArgumentException();
            DeleteFromHospitalizationTypes(equipmentType);
            DeleteFromProcedureTypes(equipmentType);
            equipmentTypeRepository.Repository.Delete(equipmentType);
        }

        private void DeleteFromHospitalizationTypes(EquipmentType equipmentType)
        {
            throw new NotImplementedException();
        }

        private void DeleteFromProcedureTypes(EquipmentType equipmentType)
        {
            throw new NotImplementedException();
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