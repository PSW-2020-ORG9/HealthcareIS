using General;
using Microsoft.EntityFrameworkCore;

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
