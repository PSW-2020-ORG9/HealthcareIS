using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;

namespace OcelotApiGateway
{
    public class Program
    {
        private static readonly string ApiUrl = Environment.GetEnvironmentVariable("PSW_API_GATEWAY_HOST")
                                                + Environment.GetEnvironmentVariable("PSW_API_GATEWAY_PORT");
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables()
                        .AddJsonFile("ocelot.global.json");

                })
            .Build().Run();
        }
        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseUrls(ApiUrl); });

    }
}