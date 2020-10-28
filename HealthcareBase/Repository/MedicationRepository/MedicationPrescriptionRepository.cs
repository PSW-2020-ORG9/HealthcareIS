// File:    MedicationPrescriptionRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:26
// Purpose: Definition of Interface MedicationPrescriptionRepository

using Model.Medication;
using Repository.Generics;
using System;
using System.Collections.Generic;
using Model.Users.Patient;

namespace Repository.MedicationRepository
{
    public interface MedicationPrescriptionRepository : Repository<MedicationPrescription, int>
    {
        IEnumerable<MedicationPrescription> GetByPatient(Patient patient);

    }
}