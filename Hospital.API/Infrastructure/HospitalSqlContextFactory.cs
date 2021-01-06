using General;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Infrastructure
{
    public class HospitalSqlContextFactory : IContextFactory
    {
        private readonly string _connectionString;

        public HospitalSqlContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext() => new HospitalSqlContext(_connectionString);
    }
}
