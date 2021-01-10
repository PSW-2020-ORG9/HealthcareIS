using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using General.Auth;
using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;

namespace OcelotApiGateway.Auth
{
    public static class OcelotJwtMiddleware
    {
        private static readonly string RoleSeparator = ",";
        
        public static Func<DownstreamContext, Func<Task>, Task> CreateAuthorizationFilter 
            => async (downStreamContext, next) =>
            {
                HttpContext httpContext = downStreamContext.HttpContext;
                var token = httpContext.Request.Cookies[JwtManager.AuthorizationTokenKey];
                if (AuthorizeIfValidToken(downStreamContext, token))
                {
                    await next.Invoke();
                }
                else
                {
                    downStreamContext.DownstreamResponse =
                        new DownstreamResponse(new HttpResponseMessage(HttpStatusCode.Unauthorized));
                }
            };
        
        private static bool AuthorizeIfValidToken(DownstreamContext downStreamContext, string jwtToken)
        {
            downStreamContext.DownstreamReRoute.RouteClaimsRequirement
                .TryGetValue("Role", out string allowedRoles);
            if (allowedRoles == null)
                return true;

            if (jwtToken == null)
                return false;
            
            IIdentityProvider decodedObject = new JwtManager().Decode<UserToken>(jwtToken);
            if (decodedObject != null)
            {
                return allowedRoles
                    .Split(RoleSeparator)
                    .FirstOrDefault(role => role.Trim() == decodedObject.GetRole()) != default;
            }

            return false;
        }
    }
}