using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Infrastructure
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
