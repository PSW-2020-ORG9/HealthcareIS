using Hospital.API.Infrastructure;
using Hospital.API.Model.Medication;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.IntegrationTests.Context
{
    internal class HospitalSqlTestContext : HospitalSqlContext
    {
        public HospitalSqlTestContext(string connectionString) : base(connectionString) { }

        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medication>().HasData(new Medication
            {
                Id = 1,
                Name = "Brufen"
            });
            modelBuilder.Entity<IntakeInstructions>().HasData(new IntakeInstructions
            {
                Id = 1,
            });
            modelBuilder.Entity<MedicationPrescription>().HasData(new MedicationPrescription
            {
                Id = 1,
                DiagnosisId = 1,
                ExaminationReportId = 1,
                InstructionsId = 1,
                MedicationId = 1
            });
        }
    }
}
