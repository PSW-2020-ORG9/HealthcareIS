using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;

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
            return new MySqlTestContext(_connectionString);
        }
    }
}
