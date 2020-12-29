using Microsoft.EntityFrameworkCore;
using Schedule.API.Infrastructure.Database;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Shifts;
using System;

namespace Schedule.API.IntegrationTests.Context
{
    class ScheduleSqlTestContext : ScheduleSqlContext
    {
        public ScheduleSqlTestContext(string connectionString) : base(connectionString) { }

        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnosis>().HasData(new Diagnosis
            {
                Id = 1,
            });
            modelBuilder.Entity<Examination>(e =>
            {
                e.HasData(new Examination
                {
                    Id = 1,
                    PatientId = 1
                });
                e.OwnsOne(x => x.TimeInterval).HasData(new
                {
                    ExaminationId = 1,
                    Start = new DateTime(2022, 2, 2, 8, 0, 0),
                    End = new DateTime(2022, 2, 2, 8, 30, 0)
                });
                e.HasData(new Examination
                {
                    Id = 2,
                });
                e.OwnsOne(x => x.TimeInterval).HasData(new
                {
                    ExaminationId = 2,
                    Start = new DateTime(2022, 1, 1, 8, 0, 0),
                    End = new DateTime(2022, 1, 1, 8, 30, 0)
                });
            });
            modelBuilder.Entity<Shift>(s =>
            {
                s.HasData(new Shift
                {
                    Id = 1,
                    AssignedExamRoomId = 1,
                    DoctorId = 1
                });
                s.OwnsOne(x => x.TimeInterval).HasData(new
                {
                    ShiftId = 1,
                    Start = new DateTime(2022, 3, 3),
                    End = new DateTime(2022, 3, 3)
                });
            });
        }
    }
}
