using Microsoft.EntityFrameworkCore;

namespace Schedule.API.Infrastructure.Database
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
