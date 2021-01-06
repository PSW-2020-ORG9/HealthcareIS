using General;
using Microsoft.EntityFrameworkCore;

namespace OcelotApiGateway.Model.Infrastructure
{
    public class EventStoreSqlContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public EventStoreSqlContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            return new EventStoreSqlContext(_connectionString);
        }
    }
}
