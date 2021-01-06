using General;
using Microsoft.EntityFrameworkCore;

namespace Schedule.API.Infrastructure.Database
{
    public class MySqlContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public MySqlContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            return new ScheduleSqlContext(_connectionString);
        }
    }
}
