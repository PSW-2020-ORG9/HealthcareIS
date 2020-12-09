// File:    PatientRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientRepository

using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface
{
    public interface IPatientRepository : IWrappableRepository<Patient, int>
    {
        bool ExistsByJMBG(string jmbg);
        Patient GetByJMBG(string jmbg);
    }
}