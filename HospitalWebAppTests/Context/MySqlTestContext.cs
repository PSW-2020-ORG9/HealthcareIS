using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalWebAppIntegrationTests.Context
{
    internal class MySqlTestContext : MySqlContext
    {
        public MySqlTestContext() { }
        public MySqlTestContext(string connectionString) : base(connectionString) { }
        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(new Country
            {
                Id = 1,
                Name = "Srbija",
                Code = "RS"
            });
            modelBuilder.Entity<City>().HasData(new City
            {
                Id = 1,
                CountryId = 1,
                PostalCode = "21000",
                Name = "Novi Sad"
            });
            modelBuilder.Entity<Person>().HasData(new Person
            {
                CountryOfBirthId = 1,
                CountryOfResidenceId = 1,
                Jmbg = "123",
                CityOfBirthId = 1,
                CityOfResidenceId = 1,
                Gender = Gender.Other
            });
            modelBuilder.Entity<MedicalRecord>().HasData(new MedicalRecord
            {
                Id = 1
            });
            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                Id = 1,
                Jmbg = "123",
                InsuranceNumber = "124125",
                MedicalRecordId = 1
            });
        }
    }
}
