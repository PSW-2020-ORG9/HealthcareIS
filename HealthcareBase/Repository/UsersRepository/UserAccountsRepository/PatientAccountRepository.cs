// File:    PatientAccountRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientAccountRepository

using Model.Users.Patient;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;

namespace Repository.UsersRepository.UserAccountsRepository
{
    public interface PatientAccountRepository : Repository<PatientAccount, int>
    {
        Boolean ExistsByJMBG(String jmbg);

        Boolean IsUsernameUnique(String username);

        String GetPasswordByUsername(String username);

        PatientAccount GetByUsernameAndPassword(String username, String password);

        PatientAccount GetByPatient(Patient patient);

    }
}