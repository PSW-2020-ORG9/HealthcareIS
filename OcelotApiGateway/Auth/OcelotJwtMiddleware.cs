using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using General.Auth;
using Microsoft.AspNetCore.Http;
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
                if (token != null)
                    AuthorizeIfValidToken(downStreamContext, token);

                await next.Invoke();
            };
        
        private static void AuthorizeIfValidToken(DownstreamContext downstreamContext, string jwtToken)
        {
            IIdentityProvider decodedObject = new JwtManager().Decode<UserToken>(jwtToken);
            if (decodedObject != null)
            {
                downstreamContext.HttpContext.User.AddIdentity(new ClaimsIdentity(new []
                {
                    new Claim("Role", decodedObject.GetRole())
                }));
            }
        }
    }
}