// File:    MedicationService.cs
// Author:  Korisnik
// Created: 25 May 2020 13:42:22
// Purpose: Definition of Class MedicationService

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Dto;
using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;

namespace HealthcareBase.Service.MedicationService
{
    public class MedicationService
    {
        private readonly RepositoryWrapper<IMedicationRepository> medicationRepository;

        public MedicationService(IMedicationRepository medicationRepository)
        {
            this.medicationRepository = new RepositoryWrapper<IMedicationRepository>(medicationRepository);
        }

        public Medication GetByID(int id)
        {
            return medicationRepository.Repository.GetByID(id);
        }

        public IEnumerable<MedicationDto> GetAll()
        {
            return medicationRepository.Repository.GetColumnsForMatching(
               condition: medication => medication.Id != 0,
               selection: medication => new MedicationDto()
               {
                   Id = medication.Id,
                   Description = medication.Description,
                   Name = medication.Name,
                   Quantity = 0,
                   Type = medication.Type
               }
           );
        }
        public IEnumerable<MedicationDto> GetAllMedicineWithQuantity()
        {
            Dictionary<string, MedicationDto> allMedication = new Dictionary<string, MedicationDto>();
            foreach (MedicationDto medication in GetAll())
            {
                if (!allMedication.ContainsKey(medication.Name))
                {
                    allMedication[medication.Name] = medication;

                }
                allMedication[medication.Name].Quantity += 1;
            }
            return allMedication.Values.ToList();
        }
    }
}