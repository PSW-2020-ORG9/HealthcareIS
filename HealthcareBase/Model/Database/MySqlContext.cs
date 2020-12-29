using EntityFramework.Exceptions.MySQL.Pomelo;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship;
using HealthcareBase.Model.Users.Survey;
using HealthcareBase.Model.Users.Survey.SurveyEntry;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserFeedback;
using Microsoft.EntityFrameworkCore;


namespace HealthcareBase.Model.Database
{
    public class MySqlContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string db = "baza";
        private readonly string pass = "1234567";

        public MySqlContext()
        {
            _connectionString = "server=localhost;port=3306;database=" + db + ";user=root;password=" + pass;
        }

        public MySqlContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
            optionsBuilder.UseExceptionProcessor();
        }

        // Rooms, Hospitalizations, Equipment, etc.
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentUnit> EquipmentUnits { get; set; }
        public DbSet<MedicalConsumable> MedicalConsumables { get; set; }
        public DbSet<MedicalConsumableType> MedicalConsumableTypes { get; set; }
        public DbSet<Renovation> Renovations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Medication.Medication> Medications { get; set; }
        
        public DbSet<ProcedureDetails> ProcedureDetails { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<RatedSurveySection> RatedSurveySections { get; set; }
        public DbSet<RatedSurveyQuestion> RatedSurveyQuestions { get; set; }
        public DbSet<DoctorSurveySection> doctorSurveySections { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<PatientSurveyResponse> PatientSurveyResponses { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<FavoriteDoctor> FavoriteDoctors { get; set; }
        
        // Allergies 
        public DbSet<AllergyManifestation> AllergyManifestations { get; set; }
        public DbSet<Allergy> Allergies { get; set; }

        // Patient general
        public DbSet<Patient> Patients { get; set; }

        // Procedures
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<ExaminationReport> ExaminationReports { get; set; }
        public DbSet<MedicationPrescription> MedicationPrescriptions { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        
        // Accounts 
        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<DoctorAccount> DoctorAccounts { get; set; }
        public DbSet<AdministrationAccount> AdministrationAccounts { get; set; }
        
        // Staff
        public DbSet<AdministrationWorker> AdministrationWorkers { get; set; }

        // Doctors
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            SetRelations(modelBuilder);
            SetCompositeKeys(modelBuilder);
        }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {
            
        }

        private static void SetRelations(ModelBuilder modelBuilder)
        {
            // TODO Set default Restrict
        }

        private static void SetCompositeKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizenship>()
                .HasKey(c => new {c.CountryID, c.PersonJmbg});
            modelBuilder.Entity<FavoriteDoctor>()
                .HasKey(fav => new {fav.DoctorId, fav.PatientAccountId});
            modelBuilder.Entity<AllergyManifestation>()
                .HasKey(am => new {am.PatientId, am.AllergyId});
            modelBuilder.Entity<DoctorSpecialty>()
                .HasKey(ds => new {ds.DoctorId, ds.SpecialtyId});
        }
    }
}