using Microsoft.EntityFrameworkCore;
using System;
using User.API.Infrastructure;
using User.API.Model.Generalities;
using User.API.Model.Locale;
using User.API.Model.Users.Employees.Doctors;
using User.API.Model.Users.Patients;
using User.API.Model.Users.UserAccounts;

namespace User.API.IntegrationTests.Context
{
    class UserSqlTestContext : UserSqlContext
    {
        public UserSqlTestContext(string connectionString) : base(connectionString) { }

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
                Name = "Novi Sad",
                PostalCode = "21000"
            });
            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = "1",
                CityOfBirthId = 1,
                CityOfResidenceId = 1,
            });
            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                Id = 1,
                PersonId = "1"
            });
            modelBuilder.Entity<PatientAccount>().HasData(new PatientAccount
            {
                Id = 1,
                IsActivated = false,
                PatientId = 1,
                UserGuid = new Guid("00000000-0000-0000-0000-000000000000"),
            });
            modelBuilder.Entity<Specialty>().HasData(new Specialty
            {
                Id = 1,
            });
            modelBuilder.Entity<DoctorSpecialty>().HasData(new DoctorSpecialty
            {
                DoctorId = 1,
                SpecialtyId = 1
            });
            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = 1,
            });
            modelBuilder.Entity<Doctor>().HasData(new Doctor
            {
                Id = 1,
                DepartmentId = 1,
                PersonId = "1",
            });
        }
    }
}
