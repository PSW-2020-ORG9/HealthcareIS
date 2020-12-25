using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Infrastructure
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
            return new MySqlContext(_connectionString);
        }
    }
}
