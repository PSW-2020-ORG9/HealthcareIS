using General;
using Microsoft.EntityFrameworkCore;

namespace User.API.Infrastructure
{
    public class UserSqlContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public UserSqlContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            return new UserSqlContext(_connectionString);
        }
    }
}
