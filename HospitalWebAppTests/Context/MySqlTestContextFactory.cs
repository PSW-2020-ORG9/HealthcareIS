using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalWebAppIntegrationTests.Context
{
    internal class MySqlTestContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public MySqlTestContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            DbContext context = new MySqlTestContext(_connectionString);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
