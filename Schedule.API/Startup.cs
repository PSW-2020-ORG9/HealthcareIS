using System;
using General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.API.Infrastructure.Database;
using Schedule.API.Infrastructure.Repositories.Procedures;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Services.Procedures;
using Schedule.API.Services.Procedures.Interface;
using IExaminationService = Schedule.API.Services.Procedures.IExaminationService;

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

        private const string UserUrl = "http://localhost:5003/";
        private const string HospitalUrl = "http://localhost:5004/";

        public void ConfigureServices(IServiceCollection services)
        {
            // Examinations
            IExaminationRepository examinationRepository = new ExaminationSqlRepository(GetContextFactory());
            IShiftRepository shiftRepository = new ShiftSqlRepository(GetContextFactory());
            IExaminationService examinationService = new IExaminationService(examinationRepository, shiftRepository);
            
            IConnection patientConnection = new Connection(UserUrl, "patient");
            IConnection doctorConnection = new Connection(UserUrl, "doctor");
            IConnection roomConnection = new Connection(HospitalUrl, "room");
            ExaminationServiceProxy examinationServiceProxy = 
                new ExaminationServiceProxy(
                    examinationService,
                    roomConnection, doctorConnection, patientConnection);

            // Recommendations
            RecommendationService recommendationService =
                new RecommendationService(examinationRepository, shiftRepository, doctorConnection);
            
            // Diagnoses
            IDiagnosisRepository diagnosisRepository = new DiagnosisSqlRepository(GetContextFactory());
            IDiagnosisService diagnosisService = new DiagnosisService(diagnosisRepository);
            
            services.Add(new ServiceDescriptor(typeof(IDiagnosisService), diagnosisService));
            services.Add(new ServiceDescriptor(typeof(ExaminationServiceProxy), examinationServiceProxy));
            services.Add(new ServiceDescriptor(typeof(RecommendationService), recommendationService));
            
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