using Hospital.API.Model.ESProjection;
using Hospital.API.Model.Medication;
using Hospital.API.Model.Resources;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Infrastructure
{
    public class HospitalSqlContext : DbContext
    {
        private readonly string _connectionString;
        public HospitalSqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IntakeInstructions> IntakeInstructions { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationPrescription> MedicationPrescriptions { get; set; }
        public DbSet<SideEffect> SideEffects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentUnit> EquipmentUnits { get; set; }
        public DbSet<MedicalConsumable> MedicalConsumables { get; set; }
        public DbSet<MedicalConsumableType> MedicalConsumableTypes { get; set; }
        public DbSet<Renovation> Renovations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        //ES - Projection
        public DbSet<Projection> Projection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore(typeof(Diagnosis));

            SeedData(modelBuilder);
        }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {

        }
    }
}
