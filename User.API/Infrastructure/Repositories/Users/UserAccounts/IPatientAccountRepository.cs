// File:    PatientAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientAccountRepository


using General.Repository;
using User.API.Model.Users.Patients;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure.Repositories.Users.UserAccounts
{
    public interface IPatientAccountRepository : IWrappableRepository<PatientAccount, int>
    {
        PatientAccount GetByPatient(Patient patient);
        PatientAccount GetByPatientId(int id);
    }
}