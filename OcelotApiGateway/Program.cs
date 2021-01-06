using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using System.Text.RegularExpressions;

namespace OcelotApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);

            builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables()
                        .AddJsonStream(OcelotGlobalConfiguration("ocelot.global.json"));

                })
            .Build().Run();
        }
        
        public static Stream OcelotGlobalConfiguration(String filePath)
        {
            String configuration = File.ReadAllText(filePath);
            foreach (Match match in Regex.Matches(configuration, "[$][A-Z_]+"))
                configuration = configuration.Replace(match.Value, Environment.GetEnvironmentVariable(match.Value.Split('$')[1]));
            return new MemoryStream(Encoding.UTF8.GetBytes(configuration));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var port = Environment.GetEnvironmentVariable("PORT")
                               ?? Environment.GetEnvironmentVariable("PSW_API_GATEWAY_PORT");
                    if(port == null) webBuilder.UseStartup<Startup>();
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:" + port);
                });

    }
}