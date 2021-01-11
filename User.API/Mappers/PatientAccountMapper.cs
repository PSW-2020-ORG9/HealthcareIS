using System;
using User.API.Model.Generalities;
using User.API.Model.Users.Patients;
using User.API.Model.Users.UserAccounts;
using User.API.Model.Users.UserAccounts.Registration;


namespace User.API.Mappers
{
    public static class PatientAccountMapper
    {
        public static PatientAccount DtoToObject(PatientRegistrationDTO dto)
        {
            return new PatientAccount
            {
                AvatarUrl = dto.AvatarUrl,
                Credentials = new Credentials
                {
                    Email = dto.Email,
                    Password = dto.Password,
                    Username = dto.Username
                },
                Role = "Patient",
                UserGuid = Guid.NewGuid(),
                IsActivated = false,
                RespondedToSurvey = false,
                Patient = new Patient
                {
                    PersonId = dto.Jmbg,
                    InsuranceNumber = dto.InsuranceNumber,
                    Status = PatientStatus.Alive,
                    Allergies = dto.Allergies,
                    Person = new Person
                    {
                        Address = dto.Address,
                        Age = dto.Age,
                        Citizenships = dto.Citizenships,
                        CityOfBirthId = dto.CityOfBirthId,
                        CityOfResidenceId = dto.CityOfResidenceId,
                        DateOfBirth = dto.DateOfBirth,
                        Gender = dto.Gender,
                        Id = dto.Jmbg,
                        Name = dto.Name,
                        Surname = dto.Surname,
                        TelephoneNumber = dto.TelephoneNumber,
                        MiddleName = dto.MiddleName,
                        MaritalStatus = dto.MaritalStatus,
                    },
                }
            };
        }
    }
}