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
            var allowedRoles = GetAllowedRolesForCurrentRoute(downStreamContext);

            if (allowedRoles == null) return;
            if (jwtToken == null) throw new UnauthorizedAccessException();
            
            IIdentityProvider identityProvider = new JwtManager().Decode<UserToken>(jwtToken);
            if (identityProvider != null && IsIdentityRoleAllowed(allowedRoles, identityProvider))
            {
                AppendUserIdToRequest(downStreamContext.DownstreamRequest, identityProvider.GetUserId());
                return;
            }

            throw new UnauthorizedAccessException();
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

        private static void AppendUserIdToRequest(DownstreamRequest request, int userId)
        {
            request.Headers.Add(Constants.UserIdHeaderKey, userId.ToString());
        }
    }
}