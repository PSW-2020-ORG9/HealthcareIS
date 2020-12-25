using Microsoft.EntityFrameworkCore;

namespace User.API.Infrastructure
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
