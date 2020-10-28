// File:    PatientRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientRepository

using Model.Users.Patient;
using Repository.Generics;
using System;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface PatientRepository : Repository<Patient, int>
    {
        Boolean ExistsByJMBG(String jmbg);

        Patient GetByJMBG(String jmbg);

    }
}