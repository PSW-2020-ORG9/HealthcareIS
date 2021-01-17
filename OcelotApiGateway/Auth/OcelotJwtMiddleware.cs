using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using General.Auth;
using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;

namespace OcelotApiGateway.Auth
{
    public static class OcelotJwtMiddleware
    {
        private static readonly string RoleSeparator = ",";
        
        public static Func<DownstreamContext, Func<Task>, Task> CreateAuthorizationFilter 
            => async (downStreamContext, next) =>
            {
                string jwtToken = HttpIdentityHandler.GetJwtFromRequest(downStreamContext.HttpContext.Request);
                try
                {
                    TryAuthorizeWithToken(downStreamContext, jwtToken);
                    await next.Invoke();
                }
                catch (UnauthorizedAccessException)
                {
                    downStreamContext.DownstreamResponse =
                        new DownstreamResponse(new HttpResponseMessage(HttpStatusCode.Unauthorized));
                }
            };
        
        private static void TryAuthorizeWithToken(DownstreamContext downStreamContext, string jwtToken)
        {
            IIdentityProvider identityProvider = null;
            if (jwtToken != null)
            {
                JwtManager jwtManager = new JwtManager(GetJwtSecretFromEnvironment());
                identityProvider = jwtManager.Decode<UserToken>(jwtToken);
                if (identityProvider != null)
                    AppendUserInfoToRequest(downStreamContext.DownstreamRequest, identityProvider);
            }

            var allowedRoles = GetAllowedRolesForCurrentRoute(downStreamContext);
            if (allowedRoles == null)
                return;
            else if (identityProvider == null || !IsIdentityRoleAllowed(allowedRoles, identityProvider))
                throw new UnauthorizedAccessException();
        }

        private static string GetJwtSecretFromEnvironment()
        {
            string jwtSecret = Environment.GetEnvironmentVariable("PSW_JWT_SECRET");
            if (jwtSecret == default) throw new ApplicationException("JWT secret environment variable not set.");
            else return jwtSecret;
        }

        private static string GetAllowedRolesForCurrentRoute(DownstreamContext downstreamContext)
        {
            downstreamContext.DownstreamReRoute.RouteClaimsRequirement
                .TryGetValue(Constants.AuthorizationAttributeKey, out string allowedRoles);
            return allowedRoles;
        }

        private static bool IsIdentityRoleAllowed(string allowedRoles, IIdentityProvider identityProvider)
        {
            return allowedRoles
                .Split(RoleSeparator)
                .FirstOrDefault(role => role.Trim() == identityProvider.GetRole()) != default;
        }

        private static void AppendUserInfoToRequest(DownstreamRequest request, IIdentityProvider identityProvider)
        {
            request.Headers.Add(Constants.UserIdHeaderKey, identityProvider.GetUserId().ToString());
            request.Headers.Add(Constants.UsernameHeaderKey, identityProvider.getUsername());
        }
    }
}