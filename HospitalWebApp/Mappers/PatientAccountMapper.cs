using System;
using System.Collections.Generic;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserAccounts.Registration;

namespace HospitalWebApp.Mappers
{
    public class PatientAccountMapper
    {
        public static PatientAccount DtoToObject(PatientRegistrationDTO dto)
        {
            return new PatientAccount
            {
                AvatarUrl = dto.AvatarUrl,
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                UserGuid = Guid.NewGuid(),
                FavouriteDoctors = new List<FavoriteDoctor>(),
                IsActivated = false,
                RespondedToSurvey = false,
                Patient = new Patient
                {
                    Jmbg = dto.Jmbg,
                    InsuranceNumber = dto.InsuranceNumber,
                    Status = PatientStatus.Alive,
                    Person = new Person
                    {
                        Address = dto.Address,
                        Age = dto.Age,
                        Citizenships = dto.Citizenships,
                        CityOfBirthId = dto.CityOfBirthId,
                        CityOfResidenceId = dto.CityOfResidenceId,
                        CountryOfBirthId = dto.CountryOfBirthId,
                        CountryOfResidenceId = dto.CountryOfResidenceId,
                        DateOfBirth = dto.DateOfBirth,
                        Gender = dto.Gender,
                        Jmbg = dto.Jmbg,
                        Name = dto.Name,
                        Surname = dto.Surname,
                        TelephoneNumber = dto.TelephoneNumber,
                        MiddleName = dto.MiddleName,
                        MaritalStatus = dto.MaritalStatus,
                    },
                    MedicalRecord = new MedicalRecord
                    {
                        Allergies = dto.Allergies
                    }
                }
            };
        }
    }
}