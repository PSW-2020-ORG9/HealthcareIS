using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Database
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
