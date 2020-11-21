using System;
using System.Collections.Generic;
using System.Text.Json;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedDotorSectionRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedQuestionRepository;
using HealthcareBase.Repository.UsersRepository.SurveyRepository.SurveyEntryRepository.RatedSectionRepository;
using HealthcareBase.Repository.UsersRepository.UserFeedbackRepository;
using HealthcareBase.Service.UsersService.EmployeeService;
using HealthcareBase.Service.UsersService.UserFeedbackService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService;
using HealthcareBase.Service.UsersService.UserFeedbackService.SurveyService.SurveyEntryService;
using HospitalWebApp.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace HospitalWebApp
{
    public class Startup
    {
        private string _connectionString;

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("connections.json");
            Configuration = builder.Build();
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called at runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connectionString = Configuration["MySql"];
            AddService(services, typeof(UserFeedbackService), typeof(UserFeedbackSqlRepository));
            AddSurveyServices(services);
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
            var surveyPreviewBuilder=Activator.CreateInstance(typeof(SurveyPreviewBuilder), ratedSectionService,
                surveyService, doctorService);
                
            services.Add(new ServiceDescriptor(typeof(SurveyPreviewBuilder),surveyPreviewBuilder));
        }

        private object? AddService(IServiceCollection services, Type serviceClass, Type repositoryClass)
        {
            var repository = CreateRepository(repositoryClass);
            var service = Activator.CreateInstance(serviceClass, repository);
            services.Add(new ServiceDescriptor(serviceClass, service));
            return service;
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
    }
}