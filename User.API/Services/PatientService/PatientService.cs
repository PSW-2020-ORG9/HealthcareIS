// File:    PatientService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientService

using General.Repository;
using System.Collections.Generic;
using System.Linq;
using User.API.Infrastructure.Repositories;
using User.API.Infrastructure.Repositories.Users.Patients.Interfaces;
using User.API.Model.Users.Patients;

namespace User.API.Services.PatientService
{
    public class PatientService
    {
        private readonly RepositoryWrapper<IPatientRepository> _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this._patientRepository = new RepositoryWrapper<IPatientRepository>(patientRepository);
        }

        public Patient GetByID(int id)
            => _patientRepository.Repository.GetByID(id);

        public IEnumerable<Patient> GetAllActive()
            => _patientRepository.Repository.GetMatching(patient => patient.Status == PatientStatus.Alive);

        public IEnumerable<Patient> Find(IEnumerable<int> patientIds)
            => _patientRepository.Repository.GetMatching(patient => patientIds.Contains(patient.Id));
    }
}