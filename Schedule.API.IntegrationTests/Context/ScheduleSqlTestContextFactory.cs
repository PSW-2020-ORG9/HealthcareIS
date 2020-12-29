using General;
using Microsoft.EntityFrameworkCore;

namespace Schedule.API.IntegrationTests.Context
{
    class ScheduleSqlTestContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public ScheduleSqlTestContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext() => new ScheduleSqlTestContext(_connectionString);
    }
}
