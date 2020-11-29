// File:    PatientAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientAccountRepository

using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public interface IPatientAccountRepository : IWrappableRepository<PatientAccount, int>
    {
        bool ExistsByJMBG(string jmbg);

        bool IsUsernameUnique(string username);

        string GetPasswordByUsername(string username);

        PatientAccount GetByUsernameAndPassword(string username, string password);

        PatientAccount GetByPatient(Patient patient);

        PatientAccount GetByPatientId(int id);
    }
}