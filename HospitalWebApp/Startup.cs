using System;
using HealthcareBase.Model.Database;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.Generics.Interface;
using HealthcareBase.Repository.MedicationRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Repository.UsersRepository.GeneralitiesRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedSectionRepository;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;
using HealthcareBase.Repository.UsersRepository.UserFeedbackRepository;
using HealthcareBase.Service.HospitalResourcesService.EquipmentService;
using HealthcareBase.Service.MedicationService;
using HealthcareBase.Service.MiscellaneousService;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HealthcareBase.Service.UsersService.EmployeeService;
using HealthcareBase.Service.UsersService.PatientService;
using HealthcareBase.Service.UsersService.RegistrationService;
using HealthcareBase.Service.UsersService.UserFeedbackService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService;
using HospitalWebApp.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Service.HospitalResourcesService.RoomService;

namespace HospitalWebApp
{
    public class Startup
    {
        private readonly string _connectionString;
        public Startup()
        {
            Configuration = GetConfiguration();
            _connectionString = CreateConnectionStringFromEnvironment() ?? Configuration["MySql"];
            if (_connectionString == null) throw new ApplicationException("Connection string is null");
        }

        protected IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("connections.json", optional: true);

            return builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called at runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            AddServices(services);
            AddCustomSerializers(services);
            ConfigureCors(services);
            services.AddControllers();
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            }));
        }

        private static void AddCustomSerializers(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DictionaryIntConverter());
            });
        }

        private void AddSurveyServices(IServiceCollection services)
        {
            var surveyService= AddService(services, typeof(SurveyService), typeof(SurveySqlRepository));
            var ratedSectionService = AddService(services, typeof(RatedSectionService), typeof(RatedSectionSqlRepository));
            var doctorService = AddService(services, typeof(DoctorService), typeof(DoctorSqlRepository));
            var surveyPreviewBuilder=Activator.CreateInstance(typeof(SurveyPreviewBuilder), surveyService,ratedSectionService,doctorService);
                
            services.Add(new ServiceDescriptor(typeof(SurveyPreviewBuilder),surveyPreviewBuilder));
        }

        private object? AddService(IServiceCollection services, Type serviceClass, Type repositoryClass)
        {
            var repository = CreateRepository(repositoryClass);
            var service = Activator.CreateInstance(serviceClass, repository);
            services.Add(new ServiceDescriptor(serviceClass, service));
            return service;
        }

        private void AddServices(IServiceCollection services)
        {
            AddSurveyServices(services);

            var patientRepository = new PatientSqlRepository(GetContext());
            var userFeedbackRepository = new UserFeedbackSqlRepository(GetContext());
            var prescriptionRepository = new MedicationPrescriptionSqlRepository(GetContext());
            var examinationRepository = new ExaminationSqlRepository(GetContext());
            var equipmentRepository = new EquipmentSqlRepository(GetContext());
            var medicationRepository = new MedicationSqlRepository(GetContext());
            var cityRepository = new CitySqlRepository(GetContext());
            var countryRepository = new CountrySqlRepository(GetContext());
            var patientAccountRepository = new PatientAccountSqlRepository(GetContext());
            var surveyResponseRepository = new SurveyResponseSqlRepository(GetContext());
            var surveyRepository = new SurveySqlRepository(GetContext());
            var shiftRepository = new ShiftSqlRepository(GetContext());
            var departmentRepository = new DepartmentSqlRepository(GetContext());
            var doctorRepository = new DoctorSqlRepository(GetContext());
            var equipmentTypeRepository = new EquipmentTypeSqlRepository(GetContext());
            
            var userFeedbackService = new UserFeedbackService(userFeedbackRepository);
            var patientService = new PatientService(patientRepository, null, null, null);
            var prescriptionService = new MedicationPrescriptionService(prescriptionRepository);
            var equipmentService = new EquipmentService(equipmentRepository);
            var equipmentTypeService = new EquipmentTypeService(equipmentTypeRepository);
            var medicationService = new MedicationService(medicationRepository);
            var patientAccountService = new PatientAccountService(patientAccountRepository);
            var patientRegistrationService = new PatientRegistrationService(patientAccountService, new RegistrationNotifier(Environment.GetEnvironmentVariable("PSW_ACTIVATION_ENDPOINT")));
            var doctorAvailabilityService = new DoctorAvailabilityService(shiftRepository,examinationRepository);
            var departmentService = new DepartmentService(departmentRepository);
            var doctorService = new DoctorService(doctorRepository);

            var examinationService = new ExaminationService(examinationRepository, shiftRepository, doctorRepository);
            var cityService = new CityService(cityRepository);
            var countryService = new CountryService(countryRepository);

            var surveyResponseService = new SurveyResponseService(surveyResponseRepository);
            var surveyService = new SurveyService(surveyRepository);
            
            var specialtyService = new SpecialtyService(specialtyRepository);
            
            services.Add(new ServiceDescriptor(typeof(UserFeedbackService), userFeedbackService));
            services.Add(new ServiceDescriptor(typeof(PatientService), patientService));
            services.Add(new ServiceDescriptor(typeof(IPatientAccountService), patientAccountService));
            services.Add(new ServiceDescriptor(typeof(MedicationPrescriptionService), prescriptionService));
            services.Add(new ServiceDescriptor(typeof(EquipmentService), equipmentService));
            services.Add(new ServiceDescriptor(typeof(EquipmentTypeService), equipmentTypeService));
            services.Add(new ServiceDescriptor(typeof(MedicationService), medicationService));
            services.Add(new ServiceDescriptor(typeof(CityService),cityService));
            services.Add(new ServiceDescriptor(typeof(CountryService),countryService));
            services.Add(new ServiceDescriptor(typeof(PatientRegistrationService), patientRegistrationService));
            services.Add(new ServiceDescriptor(typeof(ISurveyResponseService), surveyResponseService));
            services.Add(new ServiceDescriptor(typeof(ISurveyService), surveyService));
            services.Add(new ServiceDescriptor(typeof(ExaminationService), examinationService));
            services.Add(new ServiceDescriptor(typeof(DoctorAvailabilityService), doctorAvailabilityService));
            services.Add(new ServiceDescriptor(typeof(DepartmentService),departmentService));
            services.Add(new ServiceDescriptor(typeof(DoctorService),doctorService));
            services.Add(new ServiceDescriptor(typeof(SpecialtyService), specialtyService));
        }

        private IPreparable CreateRepository(Type repositoryClass)
        {
            return Activator.CreateInstance(repositoryClass, new MySqlContextFactory(_connectionString)) as IPreparable;
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

        protected virtual IContextFactory GetContext()
        {
            return new MySqlContextFactory(_connectionString);
        }

        /// <summary>
        /// Creates a connection string from environment variables.
        /// </summary>
        /// <param name="testing">Determines if the current environment is testing.</param>
        /// <returns></returns>
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
        
        
    }
}