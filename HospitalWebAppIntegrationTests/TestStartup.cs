using HealthcareBase.Model.Database;
using HospitalWebApp;
using HospitalWebAppIntegrationTests.Context;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalWebAppIntegrationTests
{
    public class TestStartup : Startup
    {
        private readonly string _connectionString;
        private static bool _databaseInitialized = false;
        private static readonly object _mutex = new object();
        public TestStartup(IWebHostEnvironment env): base(env) 
        {
            _connectionString = CreateConnectionStringFromEnvironment(true) ?? Configuration["MySqlTest"];
            if (_connectionString == null) throw new ApplicationException("Connection string is null");
            lock (_mutex)
            {
                if (_databaseInitialized) return;
                _databaseInitialized = true;
                GetContext().CreateContext().Database.EnsureDeleted();
                GetContext().CreateContext().Database.EnsureCreated();
            }
        }

        protected override IContextFactory GetContext()
        {
            return new MySqlTestContextFactory(_connectionString);
        }
    }
}
