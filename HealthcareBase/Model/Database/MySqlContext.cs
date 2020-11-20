using Microsoft.EntityFrameworkCore;
using Model.Blog;
using Model.HospitalResources;
using Model.Medication;
using Model.Miscellaneous;
using Model.Notifications;
using Model.Requests;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Schedule.SchedulingPreferences;
using Model.StorageRecords;
using Model.Users.Employee;
using Model.Users.Generalities;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Model.Users.UserFeedback;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using EntityFramework.Exceptions.MySQL.Pomelo;
using Model.Users.Patient.MedicalHistory;

namespace HealthcareBase.Model.Database
{
    class MySqlContext : DbContext
    {
        private readonly string _connectionString;

        public MySqlContext()
        {
            this._connectionString = "server=localhost;port=3306;database=psw;user=root;password=password";
        }

        public MySqlContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
            optionsBuilder.UseExceptionProcessor();
        }

        public DbSet<BlogAuthor> BlogAuthors { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentUnit> EquipmentUnits { get; set; }
        public DbSet<MedicalConsumable> MedicalConsumables { get; set; }
        public DbSet<MedicalConsumableType> MedicalConsumableTypes { get; set; }
        public DbSet<Renovation> Renovations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationPrescription> MedicationPrescriptions { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<HospitalizationNotification> HospitalizationNotifications { get; set; }
        public DbSet<MedicationPrescriptionNotification> MedicationPrescriptionNotifications { get; set; }
        public DbSet<ProcedureNotification> ProcedureNotifications { get; set; }
        public DbSet<RequestNotification> RequestNotifications { get; set; }
        public DbSet<ClearDoctorsSchedule> ClearDoctorsSchedules { get; set; }
        public DbSet<ClearRoomsSchedule> ClearRoomsSchedules { get; set; }
        public DbSet<MedicationInputRequest> MedicationInputRequests { get; set; }
        public DbSet<ScheduleHospitalization> ScheduleHospitalizations { get; set; }
        public DbSet<ScheduleProcedure> ScheduleProcedures { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<ConsumableStorageRecord> ConsumableStorageRecords { get; set; }
        public DbSet<MedicationStorageRecord> MedicationStorageRecords { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<EmployeeAccount> EmployeeAccounts { get; set; }
        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<PatientSurveyResponse> PatientSurveyResponses { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<PersonalHistory> PersonalHistories { get; set; }
        public DbSet<FamilyHistory> FamilyHistories { get; set; }
        public DbSet<AllergyManifestation> AllergyManifestations { get; set; }
        public DbSet<DiagnosisDetails> DiagnosisDetails { get; set; }
        public DbSet<FamilyMemberDiagnosis> FamilyMemberDiagnoses { get; set; }
        
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetRelations(modelBuilder);

            //SeedData(modelBuilder);
        }

        private static void SetRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Equipment)
                .WithOne(e => e.Room)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Hospitalization>()
                .HasMany(h => h.EquipmentInUse)
                .WithOne(e => e.Hospitalization)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Examination>()
                .HasMany(e => e.Prescriptions)
                .WithOne(p => p.Examination)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MedicalHistory>()
                .HasMany(m => m.Allergies)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);

            modelBuilder.Entity<PersonalHistory>()
                .HasMany(ph => ph.Diagnoses)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);

            modelBuilder.Entity<FamilyHistory>()
                .HasMany(fh => fh.Diagnoses)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new { Name = "Serbia", Code = "381", Id = 1 },
                new { Name = "Macedonia", Code = "389", Id = 2 },
                new { Name = "Albania", Code = "355", Id = 3 },
                new { Name = "Montenegro", Code = "382", Id = 4 }
            );
            modelBuilder.Entity<City>().HasData(
                new City() { Name = "Novi Sad", PostalCode = "21000", Id = 1, CountryId = 1 },
                new City() { Name = "Belgrade", PostalCode = "11000", Id = 2, CountryId = 1 },
                new City() { Name = "Skopje", Id = 3, CountryId = 2 }
            );
            modelBuilder.Entity<Patient>(patient =>
            {
                patient.HasData(
                    new Patient()
                    {
                        Name = "Milos",
                        Surname = "Milanovic",
                        CityOfResidenceId = 1,
                        MedicalRecordID = 1,
                        CityOfBirthId = 1
                    }
                );
            });
            modelBuilder.Entity<PatientAccount>().HasData(
                new PatientAccount()
                { PatientId = 1, RespondedToSurvey = false, Username = "milosmilanovic", Password = "password", Id = 1 }
            );
            modelBuilder.Entity<UserFeedback>().HasData(
                new UserFeedback() { Date = DateTime.Now, UserComment = "odlicno", UserId = 1, Id = 1 },
                new UserFeedback() { Date = DateTime.Now, UserComment = "bravo", UserId = 1, Id = 2 }
            );
        }
    }
}