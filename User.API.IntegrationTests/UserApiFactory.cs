using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using User.API.Infrastructure.Repositories.Users.Patients;
using User.API.Infrastructure.Repositories.Users.UserAccounts;
using User.API.IntegrationTests.Context;
using User.API.Services.PatientService;
using User.API.Services.RegistrationService;

namespace User.API.IntegrationTests
{
    public class UserApiFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>()
                .UseEnvironment("Testing")
                .UseSolutionRelativeContentRoot("User.API")
                .ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
        }

        private string CreateConnectionStringFromEnvironment()
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = "test_user";
            string user = Environment.GetEnvironmentVariable("DB_PSW_USER");
            string password = Environment.GetEnvironmentVariable("DB_PSW_PASSWORD");
            if (server == null
                || port == null
                || database == null
                || user == null
                || password == null)
                return null;

            return $"server={server};port={port};database={database};user={user};password={password};";
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                Mock<IRegistrationNotifier> registrationNotifier = new Mock<IRegistrationNotifier>();
                string connectionString = CreateConnectionStringFromEnvironment();
                var patientAccountRepository = new PatientAccountSqlRepository(new UserSqlTestContextFactory(connectionString));

                var patientAccountService = new PatientAccountService(patientAccountRepository);
                var patientRegistrationService = new PatientRegistrationService(patientAccountService, registrationNotifier.Object);

                services.Add(new ServiceDescriptor(typeof(IPatientRegistrationService), patientRegistrationService));
            });
        }
    }
}
