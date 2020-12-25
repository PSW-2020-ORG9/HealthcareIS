using Microsoft.EntityFrameworkCore;

namespace User.API.Infrastructure
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
            return new UserSqlContext(_connectionString);
        }
    }
}
