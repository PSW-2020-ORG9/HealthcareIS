using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Repository.Generics
{
    public class MySqlContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        
        public MySqlContextFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            return new MySqlContext(_connectionString);
        }
    }
}