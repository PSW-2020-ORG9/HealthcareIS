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
                HttpContext httpContext = downStreamContext.HttpContext;
                string jwtToken = httpContext.Request.Cookies[Constants.AuthorizationTokenKey];
                if (TryAuthorizeWithToken(downStreamContext, jwtToken))
                {
                    await next.Invoke();
                }
                else
                {
                    downStreamContext.DownstreamResponse =
                        new DownstreamResponse(new HttpResponseMessage(HttpStatusCode.Unauthorized));
                }
            };
        
        private static bool TryAuthorizeWithToken(DownstreamContext downStreamContext, string jwtToken)
        {
            downStreamContext.DownstreamReRoute.RouteClaimsRequirement
                .TryGetValue(Constants.AuthorizationAttributeKey, out string allowedRoles);
            if (allowedRoles == null)
            {
                return true;
            }

            if (jwtToken == null)
                return false;
            
            IIdentityProvider decodedObject = new JwtManager().Decode<UserToken>(jwtToken);
            if (decodedObject != null)
            {
                if (allowedRoles
                    .Split(RoleSeparator)
                    .FirstOrDefault(role => role.Trim() == decodedObject.GetRole()) != default)
                {
                    AppendUserIdToRequest(downStreamContext.DownstreamRequest, decodedObject.GetUserId());
                    return true;
                }
            }

            return false;
        }

        private static void AppendUserIdToRequest(DownstreamRequest request, int userId)
        {
            request.Headers.Add(Constants.UserIdHeaderKey, userId.ToString());
        }
    }
}