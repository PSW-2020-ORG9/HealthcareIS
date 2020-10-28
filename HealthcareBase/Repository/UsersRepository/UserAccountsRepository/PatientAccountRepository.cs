// File:    PatientAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientAccountRepository

using Model.Users.Patient;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Repository.UsersRepository.UserAccountsRepository
{
    public interface PatientAccountRepository : Repository<PatientAccount, int>
    {
        bool ExistsByJMBG(string jmbg);

        bool IsUsernameUnique(string username);

        string GetPasswordByUsername(string username);

        PatientAccount GetByUsernameAndPassword(string username, string password);

        PatientAccount GetByPatient(Patient patient);
    }
}