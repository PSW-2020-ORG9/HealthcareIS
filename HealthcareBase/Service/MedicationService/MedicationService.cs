// File:    MedicationService.cs
// Author:  Korisnik
// Created: 25 May 2020 13:42:22
// Purpose: Definition of Class MedicationService

using Model.Medication;
using Repository.MedicationRepository;
using System;
using System.Collections.Generic;

namespace Service.MedicationService
{
    public class MedicationService
    {
        private MedicationRepository medicationRepository;

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