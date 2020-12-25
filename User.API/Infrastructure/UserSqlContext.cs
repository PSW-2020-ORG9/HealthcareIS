using Microsoft.EntityFrameworkCore;
using User.API.Model.Generalities;
using User.API.Model.HospitalResources;
using User.API.Model.Locale;
using User.API.Model.Medication;
using User.API.Model.Schedule;
using User.API.Model.Users.Employees.Doctors;
using User.API.Model.Users.Patients;
using User.API.Model.Users.Patients.MedicalHistory;
using User.API.Model.Users.Patients.MedicalHistory.Relationship;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure
{
    public class UserSqlContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string db = "psw";
        private readonly string pass = "password";
        
        public UserSqlContext()
        {
            _connectionString = "server=localhost;port=3306;database=" + db + ";user=root;password=" + pass;
        }
        public UserSqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }
        // Generalities
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<Person> Persons { get; set; }
        
        // Hospital Resources
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentUnit> EquipmentUnits { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        // Locale 
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        // Medication
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationPrescription> MedicationPrescriptions { get; set; }
        
        // Schedule 
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ExaminationReport> ExaminationReports { get; set; }
        public DbSet<ProcedureDetails> ProcedureDetails { get; set; }
        
        // Users
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        
        public DbSet<Patient> Patients { get; set; }
        
        public DbSet<AllergyManifestation> AllergyManifestations { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        
        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<DoctorAccount> DoctorAccounts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetCompositeKeys(modelBuilder);
        }
        private static void SetCompositeKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizenship>()
                .HasKey(c => new {c.CountryID, c.PersonJmbg});
            modelBuilder.Entity<AllergyManifestation>()
                .HasKey(am => new {am.PatientId, am.AllergyId});
            modelBuilder.Entity<DoctorSpecialty>()
                .HasKey(ds => new {ds.DoctorId, ds.SpecialtyId});
        }
    }
    
    
}
