using General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.API.IntegrationTests.Context
{
    internal class FeedbackSqlTestContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public FeedbackSqlTestContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext() => new FeedbackSqlTestContext(_connectionString);
    }
}
