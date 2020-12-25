// File:    PatientService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientService

using System.Collections.Generic;
using User.API.Infrastructure.Repositories;
using User.API.Infrastructure.Repositories.Users.Patients.Interfaces;
using User.API.Model.Users.Patients;

namespace User.API.Services.PatientService
{
    public class PatientService
    {
        private readonly RepositoryWrapper<IPatientRepository> patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this.patientRepository = new RepositoryWrapper<IPatientRepository>(patientRepository);
        }

        public Patient GetByID(int id)
        {
            return patientRepository.Repository.GetByID(id);
        }

        public IEnumerable<Patient> GetAllActive()
        {
            return patientRepository.Repository.GetMatching(patient => patient.Status == PatientStatus.Alive);
        }
    }
}