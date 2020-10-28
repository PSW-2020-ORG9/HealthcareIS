// File:    MedicationService.cs
// Author:  Korisnik
// Created: 25 May 2020 13:42:22
// Purpose: Definition of Class MedicationService

using System.Collections.Generic;
using Model.Medication;
using Repository.MedicationRepository;

namespace Service.MedicationService
{
    public class MedicationService
    {
        private readonly MedicationRepository medicationRepository;

        public MedicationService(MedicationRepository medicationRepository)
        {
            this.medicationRepository = medicationRepository;
        }

        public Medication GetByID(int id)
        {
            return medicationRepository.GetByID(id);
        }

        public IEnumerable<Medication> GetAll()
        {
            return medicationRepository.GetAll();
        }
    }
}