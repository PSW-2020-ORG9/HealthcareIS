using HealthcareBase.Model.Database;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.MedicationRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Repository.UsersRepository.GeneralitiesRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;
using HealthcareBase.Repository.UsersRepository.UserFeedbackRepository;
using HealthcareBase.Service.HospitalResourcesService.EquipmentService;
using HealthcareBase.Service.MedicationService;
using HealthcareBase.Service.MiscellaneousService;
using HealthcareBase.Service.ScheduleService.ProcedureService;
using HealthcareBase.Service.UsersService.PatientService;
using HealthcareBase.Service.UsersService.RegistrationService;
using HealthcareBase.Service.UsersService.UserFeedbackService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using HospitalWebAppIntegrationTests.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace HospitalWebAppIntegrationTests
{
    public class HospitalWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        private string _testConnectionString;
        private IContextFactory GetContext()
        {
            return new MySqlTestContextFactory(_testConnectionString);
        }
        private string CreateConnectionStringFromEnvironment()
        {
            string server = Environment.GetEnvironmentVariable("TEST_DB_PSW_SERVER");
            string port = Environment.GetEnvironmentVariable("TEST_DB_PSW_PORT");
            string database = Environment.GetEnvironmentVariable("TEST_DB_PSW_DATABASE");
            string user = Environment.GetEnvironmentVariable("TEST_DB_PSW_USER");
            string password = Environment.GetEnvironmentVariable("TEST_DB_PSW_PASSWORD");
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
            try
            {
                _testConnectionString = CreateConnectionStringFromEnvironment();
            }
            catch { }
            if(_testConnectionString == null) _testConnectionString = "server=localhost;port=3306;database=test_clinic;user=root;password=password";
            Mock<RegistrationNotifier> registrationNotifier = new Mock<RegistrationNotifier>();

            builder.ConfigureServices(services =>
            {
                var patientRepository = new PatientSqlRepository(GetContext());
                var userFeedbackRepository = new UserFeedbackSqlRepository(GetContext());
                var prescriptionRepository = new MedicationPrescriptionSqlRepository(GetContext());
                var examinationRepository = new ExaminationSqlRepository(GetContext());
                var equipmentRepository = new EquipmentSqlRepository(GetContext());
                var cityRepository = new CitySqlRepository(GetContext());
                var countryRepository = new CountrySqlRepository(GetContext());
                var patientAccountRepository = new PatientAccountSqlRepository(GetContext());
                var surveyResponseRepository = new SurveyResponseSqlRepository(GetContext());
                var surveyRepository = new SurveySqlRepository(GetContext());
                var userFeedbackService = new UserFeedbackService(userFeedbackRepository);
                var patientService = new PatientService(patientRepository, null, null, null);
                var prescriptionService = new MedicationPrescriptionService(prescriptionRepository);
                var equipmentService = new EquipmentService(equipmentRepository);
                var patientAccountService = new PatientAccountService(patientAccountRepository);
                var patientRegistrationService = new PatientRegistrationService(patientAccountService, registrationNotifier.Object);
                var examinationService = new ExaminationSchedulingService(examinationRepository, null, null, null, TimeSpan.Zero);
                var cityService = new CityService(cityRepository);
                var countryService = new CountryService(countryRepository);
                var surveyResponseService = new SurveyResponseService(surveyResponseRepository);
                var surveyService = new SurveyService(surveyRepository);

                services.Add(new ServiceDescriptor(typeof(UserFeedbackService), userFeedbackService));
                services.Add(new ServiceDescriptor(typeof(PatientService), patientService));
                services.Add(new ServiceDescriptor(typeof(IPatientAccountService), patientAccountService));
                services.Add(new ServiceDescriptor(typeof(MedicationPrescriptionService), prescriptionService));
                services.Add(new ServiceDescriptor(typeof(ExaminationSchedulingService), examinationService));
                services.Add(new ServiceDescriptor(typeof(EquipmentService), equipmentService));
                services.Add(new ServiceDescriptor(typeof(CityService), cityService));
                services.Add(new ServiceDescriptor(typeof(CountryService), countryService));
                services.Add(new ServiceDescriptor(typeof(PatientRegistrationService), patientRegistrationService));
                services.Add(new ServiceDescriptor(typeof(ISurveyResponseService), surveyResponseService));
                services.Add(new ServiceDescriptor(typeof(ISurveyService), surveyService));
            });
        }
    }
}