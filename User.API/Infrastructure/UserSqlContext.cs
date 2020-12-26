using Microsoft.EntityFrameworkCore;
using User.API.Model.Generalities;
using User.API.Model.Locale;
using User.API.Model.Users.Employees;
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
        
        
        // Locale 
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        // Users
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Patient> Patients { get; set; }
        
        public DbSet<AllergyManifestation> AllergyManifestations { get; set; }
        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<DoctorAccount> DoctorAccounts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetCompositeKeys(modelBuilder);
            

            modelBuilder.Entity<Citizenship>()
                .HasOne(c => c.Country);
            
            modelBuilder.Entity<Person>()
                .Property(p => p.MaritalStatus).HasColumnType("nvarchar(24)");
            modelBuilder.Entity<Person>()
                .Property(p => p.Gender).HasColumnType("nvarchar(24)");
            modelBuilder.Entity<Person>()
                .HasOne(p => p.CityOfResidence);
            modelBuilder.Entity<Person>()
                .HasOne(p => p.CityOfBirth);        

            modelBuilder.Entity<City>()
                .HasOne(c => c.Country);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Department);
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Specialties);

            modelBuilder.Entity<DoctorSpecialty>()
                .HasOne(ds => ds.Specialty);
            modelBuilder.Entity<DoctorSpecialty>()
                .HasOne(ds => ds.Doctor);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Person);
            modelBuilder.Entity<Employee>()
                .Property(p=>p.Status).HasColumnType("nvarchar(24)");

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Person);
            modelBuilder.Entity<Patient>()
                .Property(p=>p.Status).HasColumnType("nvarchar(24)");
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Allergies);
            
            modelBuilder.Entity<AllergyManifestation>()
                .Property(am=>am.Intensity).HasColumnType("nvarchar(12)");
            modelBuilder.Entity<AllergyManifestation>()
                .HasOne(am => am.Allergy);

            modelBuilder.Entity<UserAccount>()
                .OwnsOne(ua => ua.Credentials);
            
            modelBuilder.Entity<DoctorAccount>()
                .HasOne(da => da.Doctor);

            modelBuilder.Entity<PatientAccount>()
                .HasOne(pa => pa.Patient);


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
