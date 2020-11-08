using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthcareBase.Model.Database
{
    public interface IContextFactory
    {
        DbContext CreateContext();
    }
}
