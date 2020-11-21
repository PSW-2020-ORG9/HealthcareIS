using System;
using System.Runtime.InteropServices;
using HealthcareBase.Model.Database;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Repository.Generics;
using Repository.UsersRepository.UserFeedbackRepository;
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
                .AddJsonFile("connections.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called at runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connectionString = Configuration["MySql"];
            AddServices(services);
            services.AddControllers();
            
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
            var userFeedbackService = new UserFeedbackService(userFeedbackRepository);
            var patientService = new PatientService(patientRepository, null, null, null);

            services.Add(new ServiceDescriptor(typeof(UserFeedbackService), userFeedbackService));
            services.Add(new ServiceDescriptor(typeof(PatientService), patientService));
        }

        private IPreparable CreateRepository(Type repositoryClass)
        {
            return Activator.CreateInstance(repositoryClass, new MySqlContextFactory(_connectionString)) as IPreparable;
        }

        private IContextFactory GetContext()
        {
            return new MySqlContextFactory(_connectionString);
        }
    }
}