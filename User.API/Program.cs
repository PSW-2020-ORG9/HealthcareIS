using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace User.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var port = Environment.GetEnvironmentVariable("PORT")
                               ?? Environment.GetEnvironmentVariable("PSW_USER_SERVICE_PORT");
                    if(port == null) webBuilder.UseStartup<Startup>();
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:" + port);
                });
    }
}