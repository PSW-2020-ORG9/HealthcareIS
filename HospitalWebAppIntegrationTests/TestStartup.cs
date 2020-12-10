using HealthcareBase.Model.Database;
using HospitalWebApp;
using HospitalWebAppIntegrationTests.Context;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalWebAppIntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IWebHostEnvironment env): base(env) {    }

        protected override IContextFactory GetContext()
        {
            return new MySqlTestContextFactory(_connectionString);
        }
    }
}
