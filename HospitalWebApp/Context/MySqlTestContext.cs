using HealthcareBase.Model.Database;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory;
using HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship;
using HealthcareBase.Model.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace HospitalWebApp.Context
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
                Jmbg = "123",
                CityOfBirthId = 1,
                CityOfResidenceId = 1,
                Gender = Gender.Other
            });
            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                Id = 1,
                Jmbg = "123",
                InsuranceNumber = "124125",
            });
            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = 1,
                Name = "Cardiology",
                Description = "Cardiology department"
            });
            modelBuilder.Entity<Shift>(s =>
            {
                s.HasData(new Shift
                {
                    AssignedExamRoomId = 1,
                    DoctorId = 1,
                    Id = 1,
                });
                s.OwnsOne(x => x.TimeInterval).HasData(new
                {
                    ShiftId = 1,
                    Start = new DateTime(2022, 1, 1, 8, 0, 0),
                    End = new DateTime(2022, 1, 1, 16, 0, 0),
                });
            }
            );
            modelBuilder.Entity<Doctor>().HasData(new Doctor
            {
                Id = 1,
                Jmbg = "123",
                DepartmentId = 1,
                Specialties = new List<DoctorSpecialty>(),
            });
            modelBuilder.Entity<Room>().HasData(new Room
            {
                DepartmentId = 1,
                Id = 1,
                Name = "C1",
                Purpose = RoomType.SurgeryRoom
            });
            //Theses test values for examination will stop being valid on 1. Jan 2022.
            modelBuilder.Entity<Examination>(e =>
            {
                e.HasData(new Examination
                {
                    Id = 1,
                    DoctorId = 1,
                    PatientId = 1,
                    IsCanceled = false,
                    RoomId = 1,
                });
                e.OwnsOne(x => x.TimeInterval).HasData(new
                {
                    ExaminationId = 1,
                    Start = new DateTime(2022, 1, 1, 8, 0, 0),
                    End = new DateTime(2022, 1, 1, 8, 30, 0),
                });
            });
            modelBuilder.Entity<Allergy>().HasData(new Allergy
            {
                Id = 1,
                Allergen = "Peanuts",
                Symptoms = "Death",
                Prevention = "None"
            }, new Allergy
            {
                Id = 2,
                Allergen = "Dust",
                Symptoms = "Sneezing and teary eyes",
                Prevention = "Antihistamines"
            });
            modelBuilder.Entity<AllergyManifestation>().HasData(new AllergyManifestation
            {
                PatientId = 1,
                AllergyId = 1,
                Intensity = AllergyIntensity.Severe
            });
        }
    }
}
