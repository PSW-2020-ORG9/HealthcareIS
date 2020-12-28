using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace User.API
{
    public class Program
    {
        private static readonly string ApiUrl = $"http://{Environment.GetEnvironmentVariable("PSW_USER_SERVICE_HOST")}:" +
                                                $"{Environment.GetEnvironmentVariable("PSW_USER_SERVICE_PORT")}/";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseUrls(ApiUrl); });
    }
}