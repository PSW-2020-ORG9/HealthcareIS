using Microsoft.EntityFrameworkCore;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Shifts;
using System;

namespace Schedule.API.Infrastructure.Database
{
    public class ScheduleSqlContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string db = "psw";
        private readonly string pass = "password";
        
        public ScheduleSqlContext()
        {
            _connectionString = "server=localhost;port=3306;database=" + db + ";user=root;password=" + pass;
        }
        public ScheduleSqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore(typeof(Specialty));
            modelBuilder.Ignore(typeof(Doctor));
            modelBuilder.Ignore(typeof(MedicationPrescription));
            modelBuilder.Ignore(typeof(Room));
            modelBuilder.Ignore(typeof(Patient));

            SeedData(modelBuilder);
        }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ExaminationReport> ExaminationReports { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
    
    
}
