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
using System.Text;

namespace HealthcareBase.Model.Database
{
    class MySqlContext : DbContext
    {
        private static string password = "helloworldNOVISAD021";
        private static string port = "3306";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;port=3306;database=clinic;uid=root;password=" + password);
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
    }
}
