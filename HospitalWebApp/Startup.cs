using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Generics;
using Repository.UsersRepository.UserFeedbackRepository;
using Service.UsersService.UserFeedbackService;

namespace HospitalWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("connections.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration["MySql"];
            RepositoryWrapper<UserFeedbackSqlRepository> userFeedbackRepository = new RepositoryWrapper<UserFeedbackSqlRepository>(new MySqlContextFactory(connectionString));
            services.Add(new ServiceDescriptor(typeof(UserFeedbackService), new UserFeedbackService(userFeedbackRepository)));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}