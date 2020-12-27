using System;
using General;
using Hospital.API.Infrastructure;
using Hospital.API.Infrastructure.Repositories.Medications;
using Hospital.API.Infrastructure.Repositories.Resources;
using Hospital.API.Services.Medications;
using Hospital.API.Services.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hospital.API
{
    public class Startup
    {
        private string _connectionString;
        private const string ScheduleUrl = "http://localhost:5004/";
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

        protected string CreateConnectionStringFromEnvironment(bool testing = false)
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = testing ? "test_hospital" : "hospital";
            string user = Environment.GetEnvironmentVariable("DB_PSW_USER");
            string password = Environment.GetEnvironmentVariable("DB_PSW_PASSWORD");
            if (server == null
                || port == null
                || user == null
                || password == null)
                return null;

            return $"server={server};port={port};database={database};user={user};password={password};";
        }
        protected virtual IContextFactory GetContextFactory()
        {
            return new HospitalSqlContextFactory(_connectionString);
        }

        protected virtual IConnection CreateConnection(string url, string endpoint)
            => new Connection(url, endpoint);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var equipmentUnitRepository = new EquipmentUnitSqlRepository(GetContextFactory());
            var equipmentTypeRepository = new EquipmentTypeSqlRepository(GetContextFactory());
            var medicationRepository = new MedicationSqlRepository(GetContextFactory());
            var medicationPrescriptionRepository = new MedicationPrescriptionSqlRepository(GetContextFactory());
            var diagnosisConnection = CreateConnection(ScheduleUrl, "diagnosis");

            var equipmentService = new EquipmentService(equipmentUnitRepository, equipmentTypeRepository);
            var equipmentTypeService = new EquipmentTypeService(equipmentTypeRepository);
            var medicationService = new MedicationService(medicationRepository);
            var medicationPrescriptionService = new MedicationPrescriptionService(medicationPrescriptionRepository, diagnosisConnection);

            services.Add(new ServiceDescriptor(typeof(IEquipmentService), equipmentService));
            services.Add(new ServiceDescriptor(typeof(IEquipmentTypeService), equipmentTypeService));
            services.Add(new ServiceDescriptor(typeof(IMedicationService), medicationService));
            services.Add(new ServiceDescriptor(typeof(IMedicationPrescriptionService), medicationPrescriptionService));

            services.AddControllers();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}