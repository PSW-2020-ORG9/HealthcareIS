using HealthcareBase.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace Repository.Generics
{
    public class MySqlContextFactory : IContextFactory
    {
        public DbContext CreateContext()
        {
            return new MySqlContext();
        }
    }
}