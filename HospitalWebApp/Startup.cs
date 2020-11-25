using System;
using System.Runtime.InteropServices;
using HealthcareBase.Model.Database;
using HealthcareBase.Repository.MedicationRepository;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Medication;
using Newtonsoft.Json.Linq;
using Repository.Generics;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.UserFeedbackRepository;
using Service.MedicationService;
using Service.ScheduleService.ProcedureService;
using Service.UsersService.PatientService;
using Service.UsersService.UserFeedbackService;

namespace HospitalWebApp
{
    public class Startup
    {
        private string _connectionString = null;
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("connections.json", optional: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called at runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connectionString = CreateConnectionStringFromEnvironment();

            if (_connectionString == null)
                _connectionString = Configuration["MySql"];

            if (_connectionString == null) throw new ApplicationException("Missing database connection string");

            AddServices(services);
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            
        }

        private void AddService(IServiceCollection services, Type serviceClass, Type repositoryClass)
        {
            var repository = CreateRepository(repositoryClass);
            var service = Activator.CreateInstance(serviceClass, repository);
            services.Add(new ServiceDescriptor(serviceClass, service));
        }

        private void AddServices(IServiceCollection services)
        {
            var patientRepository = new PatientSqlRepository(GetContext());
            var userFeedbackRepository = new UserFeedbackSqlRepository(GetContext());
            var prescriptionRepository = new MedicationPrescriptionSqlRepository(GetContext());
            var examinationRepository = new ExaminationSqlRepository(GetContext());
            
            var userFeedbackService = new UserFeedbackService(userFeedbackRepository);
            var patientService = new PatientService(patientRepository, null, null, null);
            var prescriptionService = new MedicationPrescriptionService(prescriptionRepository);
            var examinationService = new ExaminationService(examinationRepository, null, null, null, null, TimeSpan.Zero);
            
            services.Add(new ServiceDescriptor(typeof(UserFeedbackService), userFeedbackService));
            services.Add(new ServiceDescriptor(typeof(PatientService), patientService));
            services.Add(new ServiceDescriptor(typeof(MedicationPrescriptionService), prescriptionService));
            services.Add(new ServiceDescriptor(typeof(ExaminationService), examinationService));
        }

        private IPreparable CreateRepository(Type repositoryClass)
        {
            return Activator.CreateInstance(repositoryClass, new MySqlContextFactory(_connectionString)) as IPreparable;
        }

        private IContextFactory GetContext()
        {
            return new MySqlContextFactory(_connectionString);
        }

        private string CreateConnectionStringFromEnvironment()
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = Environment.GetEnvironmentVariable("DB_PSW_DATABASE");
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
    }
}