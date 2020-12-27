using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Schedule.API.Infrastructure.Database;
using Schedule.API.Infrastructure.Repositories.Procedures;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Services.Procedures;

namespace Schedule.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
       
            PrepareDatabase();
        }
        
        private string _connectionString;

        private void PrepareDatabase()
        {
            _connectionString = CreateConnectionStringFromEnvironment() ?? Configuration["MySql"];

            if (_connectionString == null) throw new ApplicationException("Connection string is null");

            GetContextFactory().CreateContext().Database.EnsureCreated();
        }
        
        protected string CreateConnectionStringFromEnvironment(bool testing = false)
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = Environment.GetEnvironmentVariable(testing ? "DB_PSW_TEST_DATABASE" : "DB_PSW_DATABASE");
            string user = Environment.GetEnvironmentVariable("DB_PSW_USER");
            string password = Environment.GetEnvironmentVariable("DB_PSW_PASSWORD");
            if (server == null
                || port == null
                || database == null
                || user == null
                || password == null)
                return null;

            return $"server={server};port={port};database=schedule;user={user};password={password};";
        }
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IExaminationRepository examinationRepository = new ExaminationSqlRepository(GetContextFactory());
            IShiftRepository shiftRepository = new ShiftSqlRepository(GetContextFactory());
            ExaminationService examinationService = new ExaminationService(examinationRepository, shiftRepository);

            services.Add(new ServiceDescriptor(typeof(ExaminationService), examinationService));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private IContextFactory GetContextFactory()
        {
            return new MySqlContextFactory(_connectionString);
        }
    }
}