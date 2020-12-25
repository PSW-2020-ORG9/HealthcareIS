// File:    PatientRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface PatientRepository


using User.API.Model.Users.Patients;

namespace User.API.Infrastructure.Repositories.Users.Patients.Interfaces
{
    public interface IPatientRepository : IWrappableRepository<Patient, int>
    {
        bool ExistsByJMBG(string jmbg);
        Patient GetByJMBG(string jmbg);
    }
}