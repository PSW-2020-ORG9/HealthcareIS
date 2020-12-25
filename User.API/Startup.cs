using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.API.Infrastructure;
using User.API.Infrastructure.Repositories.Locale;
using User.API.Infrastructure.Repositories.Users.Employees;
using User.API.Infrastructure.Repositories.Users.Patients;
using User.API.Infrastructure.Repositories.Users.UserAccounts;
using User.API.Services.EmployeeService;
using User.API.Services.LocaleServices;
using User.API.Services.PatientService;
using User.API.Services.RegistrationService;

namespace User.API
{
    public class Startup
    {
        
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

            //GetContextFactory().CreateContext().Database.EnsureCreated();
        }
        
        private IContextFactory GetContextFactory()
        {
            return new MySqlContextFactory(_connectionString);
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

            return $"server={server};port={port};database={database};user={user};password={password};";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);;
            AddServices(services);
        }
        
        private void AddServices(IServiceCollection services)
        {
            var cityRepository = new CitySqlRepository(GetContextFactory());
            var cityService = new CityService(cityRepository);
            services.Add(new ServiceDescriptor(typeof(CityService),cityService));
            
            var countryRepository = new CountrySqlRepository(GetContextFactory());
            var countryService = new CountryService(countryRepository);
            services.Add(new ServiceDescriptor(typeof(CountryService),countryService));
            
            var doctorRepositry = new DoctorSqlRepository(GetContextFactory());
            var doctorService = new DoctorService(doctorRepositry);
            services.Add(new ServiceDescriptor(typeof(DoctorService),doctorService));
            
            var patientRepository = new PatientSqlRepository(GetContextFactory());
            var patientAccountRepository = new PatientAccountSqlRepository(GetContextFactory());
            
            var patientAccountService = new PatientAccountService(patientAccountRepository);
            var patientRegistrationService = new PatientRegistrationService(patientAccountService,
                new RegistrationNotifier(
                        Environment.GetEnvironmentVariable("PSW_ACTIVATION_ENDPOINT")));
            var patientService = new PatientService(patientRepository);
            
            services.Add(new ServiceDescriptor(typeof(IPatientAccountService), patientAccountService));
            services.Add(new ServiceDescriptor(typeof(PatientRegistrationService), patientRegistrationService));
            services.Add(new ServiceDescriptor(typeof(PatientService), patientService));
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