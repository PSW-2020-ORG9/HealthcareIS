using General;
using Microsoft.EntityFrameworkCore;

namespace Feedback.API.Infrastructure
{
    public class FeedbackSqlContextFactory : IContextFactory
    {
        private readonly string _connectionString;
        public FeedbackSqlContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbContext CreateContext()
        {
            return new FeedbackSqlContext(_connectionString);
        }
    }
}
