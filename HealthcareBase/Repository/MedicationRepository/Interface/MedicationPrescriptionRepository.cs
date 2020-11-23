// File:    MedicationPrescriptionRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:26
// Purpose: Definition of Interface MedicationPrescriptionRepository

using System.Collections.Generic;
using Model.Medication;
using Model.Users.Patient;
using Repository.Generics;

namespace Repository.MedicationRepository
{
    public interface MedicationPrescriptionRepository : IWrappableRepository<MedicationPrescription, int>
    {
    }
}