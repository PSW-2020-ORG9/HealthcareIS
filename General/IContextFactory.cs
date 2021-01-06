using Microsoft.EntityFrameworkCore;

namespace General
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
