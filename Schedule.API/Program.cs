using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Schedule.API
{
    public class Program
    {
        private static readonly string ApiUrl = $"http://{Environment.GetEnvironmentVariable("PSW_SCHEDULE_SERVICE_HOST")}:" +
                                                $"{Environment.GetEnvironmentVariable("PSW_SCHEDULE_SERVICE_PORT")}/";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseUrls(ApiUrl); ; });
    }
}