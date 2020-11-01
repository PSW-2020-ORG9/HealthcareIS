// File:    MedicationService.cs
// Author:  Korisnik
// Created: 25 May 2020 13:42:22
// Purpose: Definition of Class MedicationService

using System.Collections.Generic;
using Model.Medication;
using Repository.Generics;
using Repository.MedicationRepository;

namespace Service.MedicationService
{
    public class MedicationService
    {
        private readonly RepositoryWrapper<MedicationRepository> medicationRepository;

        public MedicationService(RepositoryWrapper<MedicationRepository> medicationRepository)
        {
            this.medicationRepository = medicationRepository;
        }

        public Medication GetByID(int id)
        {
            return medicationRepository.Repository.GetByID(id);
        }

        public IEnumerable<Medication> GetAll()
        {
            return medicationRepository.Repository.GetAll();
        }
    }
}