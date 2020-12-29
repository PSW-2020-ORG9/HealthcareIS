using General;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.IntegrationTests.Context
{
    internal class HospitalSqlTestContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public HospitalSqlTestContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext() => new HospitalSqlTestContext(_connectionString);
    }
}
