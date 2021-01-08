using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Schedule.API.Auth
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtManager _jwtUtil = new JwtManager();

        public JwtMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                AuthorizeIfValidToken(context, token);

            await _next(context);
        }
        
        private void AuthorizeIfValidToken(HttpContext context, string jwtToken)
        {
            UserToken decodedObject = _jwtUtil.Decode<UserToken>(jwtToken);
            if (decodedObject != null)
                context.Items["Account"] = decodedObject;
            
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
        }
    }
}