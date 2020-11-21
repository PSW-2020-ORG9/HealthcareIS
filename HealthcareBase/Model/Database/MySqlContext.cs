﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using EntityFramework.Exceptions.MySQL.Pomelo;
using HealthcareBase.Model.Blog;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Notifications;
using HealthcareBase.Model.Requests;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.StorageRecords;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserFeedback;

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
        public DbSet<Medication.Medication> Medications { get; set; }
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
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<RatedSurveySection> RatedSurveySections { get; set; }
        public DbSet<RatedSurveyQuestion> RatedSurveyQuestions { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<DoctorSurveySection> doctorSurveySections { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetRelations(modelBuilder);
            
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
        }
    }
}