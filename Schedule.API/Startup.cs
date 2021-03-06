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

namespace Schedule.API
{
    public class Startup
    {
        private static readonly string UserUrl = $"http://{Environment.GetEnvironmentVariable("PSW_USER_SERVICE_HOST")}:" +
                                                 $"{Environment.GetEnvironmentVariable("PSW_USER_SERVICE_PORT")}";

        private static readonly string HospitalUrl = $"http://{Environment.GetEnvironmentVariable("PSW_HOSPITAL_SERVICE_HOST")}:" +
                                                     $"{Environment.GetEnvironmentVariable("PSW_HOSPITAL_SERVICE_PORT")}";
        private string _connectionString;

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
            string database = testing ? "test_schedule" : Environment.GetEnvironmentVariable("DB_PSW_SCHEDULE_DATABASE");
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
        
        public IConfiguration Configuration { get; }

        protected virtual IConnection CreateConnection(string url, string endpoint)
            => new Connection(url, endpoint);

        public void ConfigureServices(IServiceCollection services)
        {
            // Examinations
            IExaminationRepository examinationRepository = new ExaminationSqlRepository(GetContextFactory());
            IShiftRepository shiftRepository = new ShiftSqlRepository(GetContextFactory());
            IExaminationSchedulingService examinationSchedulingService = 
                new ExaminationSchedulingService(examinationRepository, shiftRepository);

            DoctorAvailabilityService availabilityService = new DoctorAvailabilityService(shiftRepository,examinationRepository);
            ExaminationService examinationService = new ExaminationService(examinationRepository, shiftRepository);

            IConnection patientConnection = CreateConnection(UserUrl, "user/patient");
            IConnection doctorConnection = CreateConnection(UserUrl, "user/doctor");
            IConnection roomConnection = CreateConnection(HospitalUrl, "hospital/room");
            ExaminationServiceProxy examinationServiceProxy = 
                new ExaminationServiceProxy(
                    examinationService,
                    roomConnection, doctorConnection, patientConnection);

            // Recommendations
            IRecommendationService recommendationService =
                new RecommendationService(examinationRepository, shiftRepository, doctorConnection);
            
            // Diagnoses
            IDiagnosisRepository diagnosisRepository = new DiagnosisSqlRepository(GetContextFactory());
            IDiagnosisService diagnosisService = new DiagnosisService(diagnosisRepository);
            
            services.Add(new ServiceDescriptor(typeof(IDiagnosisService), diagnosisService));
            services.Add(new ServiceDescriptor(typeof(ExaminationServiceProxy), examinationServiceProxy));
            services.Add(new ServiceDescriptor(typeof(RecommendationService), recommendationService));
            services.Add(new ServiceDescriptor(typeof(DoctorAvailabilityService),availabilityService));
            services.Add(new ServiceDescriptor(typeof(IExaminationSchedulingService), examinationSchedulingService));

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

        protected virtual IContextFactory GetContextFactory()
        {
            return new MySqlContextFactory(_connectionString);
        }
    }
}