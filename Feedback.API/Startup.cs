using System;
using Feedback.API.Connections;
using Feedback.API.Infrastructure;
using Feedback.API.Infrastructure.Repositories;
using Feedback.API.Infrastructure.Repositories.SurveyEntries;
using Feedback.API.Services;
using Feedback.API.Services.SurveyEntry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Feedback.API
{
    public class Startup
    {
        private string _connectionString;
        private const string UserUrl = "http://localhost:5003/";
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
            string database = Environment.GetEnvironmentVariable(testing ? "DB_PSW_TEST_DATABASE" : "DB_PSW_DATABASE");
            string user = Environment.GetEnvironmentVariable("DB_PSW_USER");
            string password = Environment.GetEnvironmentVariable("DB_PSW_PASSWORD");
            if (server == null
                || port == null
                || database == null
                || user == null
                || password == null)
                return null;

            return $"server={server};port={port};database=feedback;user={user};password={password};";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            AddServices(services);
        }

        private void AddServices(IServiceCollection services)
        {
            var userFeedbackRepository = new UserFeedbackSqlRepository(GetContextFactory());
            var surveyRepository = new SurveySqlRepository(GetContextFactory());
            var surveyResponseRepository = new SurveyResponseSqlRepository(GetContextFactory());
            var ratedSectionSqlRepository = new RatedSectionSqlRepository(GetContextFactory());
            var patientAccountsConnection = new Connection(UserUrl, "/patient/accounts");
            var doctorConnection = new Connection(UserUrl, "/doctor");

            var userFeedbackService = new UserFeedbackService(userFeedbackRepository, patientAccountsConnection);
            var surveyService = new SurveyService(surveyRepository);
            var ratedSectionService = new RatedSectionService(ratedSectionSqlRepository);
            var surveyResponseService = new SurveyResponseService(surveyResponseRepository);
            var surveyPreviewBuilder = new SurveyPreviewBuilder(surveyService, ratedSectionService, doctorConnection);

            services.Add(new ServiceDescriptor(typeof(ISurveyResponseService), surveyResponseService));
            services.Add(new ServiceDescriptor(typeof(ISurveyService), surveyService));
            services.Add(new ServiceDescriptor(typeof(SurveyPreviewBuilder), surveyPreviewBuilder));
            services.Add(new ServiceDescriptor(typeof(IUserFeedbackService), userFeedbackService));
        }

        private IContextFactory GetContextFactory()
        {
            return new MySqlContextFactory(_connectionString);
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