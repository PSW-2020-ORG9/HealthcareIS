using General;
using Hospital.API.IntegrationTests.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Hospital.API.IntegrationTests.Stubs;

namespace Hospital.API.IntegrationTests
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
            => new HospitalSqlTestContextFactory(_connectionString);

        protected override IConnection CreateConnection(string url, string endpoint) 
            => new StubConnection();
    }
}
