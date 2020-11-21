// File:    MedicalConsumableTypeService.cs
// Author:  Lana
// Created: 28 May 2020 11:33:12
// Purpose: Definition of Class MedicalConsumableTypeService

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;

namespace HealthcareBase.Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableTypeService
    {
        private readonly RepositoryWrapper<MedicalConsumableRepository> medicalConsumableRepository;
        private readonly RepositoryWrapper<MedicalConsumableTypeRepository> medicalConsumableTypeRepository;

        public MedicalConsumableTypeService(
            MedicalConsumableTypeRepository medicalConsumableTypeRepository,
            MedicalConsumableRepository medicalConsumableRepository)
        {
            this.medicalConsumableTypeRepository =
                new RepositoryWrapper<MedicalConsumableTypeRepository>(medicalConsumableTypeRepository);
            this.medicalConsumableRepository =
                new RepositoryWrapper<MedicalConsumableRepository>(medicalConsumableRepository);
        }

        public MedicalConsumableType GetByID(int id)
        {
            return medicalConsumableTypeRepository.Repository.GetByID(id);
        }

        public IEnumerable<MedicalConsumableType> GetAll()
        {
            return medicalConsumableTypeRepository.Repository.GetAll();
        }

        public MedicalConsumableType Create(MedicalConsumableType medicalConsumableType)
        {
            return medicalConsumableTypeRepository.Repository.Create(medicalConsumableType);
        }

        public MedicalConsumableType Update(MedicalConsumableType medicalConsumableType)
        {
            return medicalConsumableTypeRepository.Repository.Update(medicalConsumableType);
        }

        public void Delete(MedicalConsumableType medicalConsumableType)
        {
            if (medicalConsumableRepository.Repository.ExistsByType(medicalConsumableType))
                throw new BadRequestException();
            medicalConsumableTypeRepository.Repository.Delete(medicalConsumableType);
        }
    }
}