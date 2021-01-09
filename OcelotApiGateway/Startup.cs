using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Auth;

namespace OcelotApiGateway
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOcelot();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var config = new OcelotPipelineConfiguration
            {
                PreAuthenticationMiddleware = async (downStreamContext, next) =>
                {
                    HttpContext httpContext = downStreamContext.HttpContext;
                    var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (token != null)
                        AuthorizeIfValidToken(downStreamContext, token);
                    
                    await next.Invoke();
                }
            };
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseOcelot(config).Wait();
        }
        
        private void AuthorizeIfValidToken(DownstreamContext context, string jwtToken)
        {
            UserToken decodedObject = new JwtManager().Decode<UserToken>(jwtToken);
            if (decodedObject != null)
            {
                context.HttpContext.User.AddIdentity(new ClaimsIdentity(new []
                {
                    new Claim("Role", decodedObject.Role)
                }));
            }
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
        }
    }
}