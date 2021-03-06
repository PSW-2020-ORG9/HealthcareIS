using System;
using General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.API.Infrastructure;
using User.API.Infrastructure.Repositories.Locale;
using User.API.Infrastructure.Repositories.Promotions;
using User.API.Infrastructure.Repositories.Users.Employees;
using User.API.Infrastructure.Repositories.Users.Patients;
using User.API.Infrastructure.Repositories.Users.UserAccounts;
using User.API.Services.CredentialsService;
using User.API.Services.EmployeeService;
using User.API.Services.LocaleServices;
using User.API.Services.PatientService;
using User.API.Services.PromotionsService;
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

            GetContextFactory().CreateContext().Database.EnsureCreated();
        }
        
        protected virtual IContextFactory GetContextFactory()
        {
            return new UserSqlContextFactory(_connectionString);
        }
        
        protected string CreateConnectionStringFromEnvironment(bool testing = false)
        {
            string server = Environment.GetEnvironmentVariable("DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("DB_PSW_PORT");
            string database = testing ? "test_user" : Environment.GetEnvironmentVariable("DB_PSW_USER_DATABASE");
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
            var registrationNotifier = new RegistrationNotifier(
                "http://" + 
                Environment.GetEnvironmentVariable("PSW_API_GATEWAY_HOST") +
                ":" + 
                Environment.GetEnvironmentVariable("PSW_API_GATEWAY_PORT") +
                "/api/patient/activate/");

            var patientAccountService = new PatientAccountService(patientAccountRepository);
            var patientRegistrationService = new PatientRegistrationService(patientAccountService, registrationNotifier);
                
            var patientService = new PatientService(patientRepository, patientAccountRepository);
            
            var specialtyRepository = new SpecialtySqlRepository(GetContextFactory());
            var specialtyService = new SpecialtyService(specialtyRepository);

            // Auth
            var userAccountRepository = new UserAccountSqlRepository(GetContextFactory());
            var credentialService = new CredentialsService(userAccountRepository, GetJwtSecretFromEnvironment());
            
            // Advertisement
            var advertisementRepository = new AdvertisementSqlRepository(GetContextFactory());
            var advertisementService = new AdvertisementService(advertisementRepository);

            services.Add(new ServiceDescriptor(typeof(CredentialsService), credentialService));
            services.Add(new ServiceDescriptor(typeof(IPatientAccountService), patientAccountService));
            services.Add(new ServiceDescriptor(typeof(IPatientRegistrationService), patientRegistrationService));
            services.Add(new ServiceDescriptor(typeof(PatientService), patientService));
            services.Add(new ServiceDescriptor(typeof(SpecialtyService),specialtyService));
            services.Add(new ServiceDescriptor(typeof(AdvertisementService),advertisementService));
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

        private string GetJwtSecretFromEnvironment()
        {
            string jwtSecret = Environment.GetEnvironmentVariable("PSW_JWT_SECRET");
            if (jwtSecret == default) throw new ApplicationException("JWT secret environment variable not set.");
            return jwtSecret;
        }
    }
}