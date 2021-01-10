using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using General.Auth;
using Microsoft.AspNetCore.Http;
using Ocelot.Authorisation;
using Ocelot.Middleware;

namespace OcelotApiGateway.Auth
{
    public static class OcelotJwtMiddleware
    {
        public static Func<DownstreamContext, Func<Task>, Task> CreateAuthorizationFilter 
            => async (downStreamContext, next) =>
            {
                HttpContext httpContext = downStreamContext.HttpContext;
                var token = httpContext.Request.Cookies[JwtManager.AuthorizationTokenKey];
                if (token != null && AuthorizeIfValidToken(downStreamContext, token))
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
            IIdentityProvider decodedObject = new JwtManager().Decode<UserToken>(jwtToken);
            if (decodedObject != null)
            {
                return downStreamContext.DownstreamReRoute.RouteClaimsRequirement["Role"]
                    .Contains(decodedObject.GetRole());
            }

            return false;
        }
    }
}