using System;
using EventStore.API.Infrastructure;
using EventStore.API.Infrastructure.Repositories;
using EventStore.API.Services;
using General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventStore.API
{
    public class Startup
    {

        private string _connectionString;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            PrepareDatabase();
        }

        protected virtual void PrepareDatabase()
        {
            _connectionString = CreateConnectionStringFromEnvironment() ?? Configuration["MySql"];

            if (_connectionString == null) throw new ApplicationException("Connection string is null");

            GetContextFactory().CreateContext().Database.EnsureCreated();
        }

        protected virtual IContextFactory GetContextFactory()
        {
            return new EventStoreSqlContextFactory(_connectionString);
        }

        protected string CreateConnectionStringFromEnvironment(bool testing = false)
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = testing ? "test_es" : Environment.GetEnvironmentVariable("DB_PSW_EVENT_STORE_DATABASE");
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            AddServices(services);
        }

        private void AddServices(IServiceCollection services)
        {
            var schedulingEventRepository = new SchedulingEventSqlRepository(GetContextFactory());
            var schedulingEventService = new SchedulingEventService(schedulingEventRepository);
            services.Add(new ServiceDescriptor(typeof(SchedulingEventService),schedulingEventService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
