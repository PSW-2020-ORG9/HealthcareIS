using General;
using Microsoft.Extensions.Configuration;
using Moq;
using Schedule.API.IntegrationTests.Context;
using Schedule.API.Model.Dependencies;
using System;
using System.Collections.Generic;

namespace Schedule.API.IntegrationTests
{
    public class TestStartup : Startup
    {
        private string _connectionString;
        private static bool _databaseInitialized = false;
        private static readonly object _mutex = new object();
        public TestStartup(IConfiguration configuration) : base(configuration) { }

        protected override void PrepareDatabase()
        {
            _connectionString = CreateConnectionStringFromEnvironment(true) ?? Configuration["MySqlTest"];
            if (_connectionString == null) throw new ApplicationException("Connection string is null");
            lock (_mutex)
            {
                if (_databaseInitialized) return;
                _databaseInitialized = true;
                GetContextFactory().CreateContext().Database.EnsureDeleted();
                GetContextFactory().CreateContext().Database.EnsureCreated();
            }
        }

        protected override IContextFactory GetContextFactory()
            => new ScheduleSqlTestContextFactory(_connectionString);

        protected override IConnection CreateConnection(string url, string endpoint)
        {
            Mock<IConnection> connection = new Mock<IConnection>();
            List<DoctorSpecialty> specialties = new List<DoctorSpecialty>();
            specialties.Add(new DoctorSpecialty
            {
                SpecialtyId = 1,
            });
            List<Doctor> doctors = new List<Doctor>();
            doctors.Add(new Doctor
            {
                Id = 1,
                Person = new Person
                {
                    Name = "Pera",
                    Surname = "Peric"
                },
                Specialties = specialties,
            });
            List<Room> rooms = new List<Room>();
            rooms.Add(new Room
            {
                Id = 1
            });
            List<Patient> patients = new List<Patient>();
            patients.Add(new Patient
            {
                Id = 1,
                Person = new Person
                {
                    Name = "Pera",
                    Surname = "Peric"
                }
            });
            connection.Setup(m => m.Get<IEnumerable<Doctor>>(It.IsAny<string>())).Returns(doctors);
            connection.Setup(m => m.Post<IEnumerable<Doctor>>(It.IsAny<object>())).Returns(doctors);
            connection.Setup(m => m.Post<IEnumerable<Room>>(It.IsAny<object>())).Returns(rooms);
            connection.Setup(m => m.Post<IEnumerable<Patient>>(It.IsAny<object>())).Returns(patients);
            return connection.Object;
        }
    }
}
