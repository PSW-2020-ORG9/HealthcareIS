using General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.API.IntegrationTests.Context
{
    class UserSqlTestContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public UserSqlTestContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
            => new UserSqlTestContext(_connectionString);
    }
}
