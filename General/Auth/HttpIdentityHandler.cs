using Microsoft.AspNetCore.Http;

namespace General.Auth
{
    public static class HttpIdentityHandler
    {
        public static string GetUserIdFromRequest(HttpRequest httpRequest)
        {
            try
            {
                return httpRequest.Headers[Constants.UserIdHeaderKey];
            }
            catch
            {
                return default;
            }
        }

        public static string GetJwtFromRequest(HttpRequest httpRequest)
        {
            return httpRequest.Cookies[Constants.AuthorizationTokenKey];
        }

        public static string GetUsernameFromRequest(HttpRequest httpRequest)
        {
            try
            {
                return httpRequest.Headers[Constants.UsernameHeaderKey];
            }
            catch
            {
                return default;
            }
        }
    }
}