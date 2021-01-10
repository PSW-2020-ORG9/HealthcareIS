using System;
using General.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.API.DTOs;
using User.API.Infrastructure.Exceptions;
using User.API.Services.CredentialsService;

namespace User.API.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly CredentialsService _credentialsService;

        public AuthController(CredentialsService credentialsService)
        {
            this._credentialsService = credentialsService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginCredentials credentials)
        {
            string authToken;
            try
            {
                authToken = _credentialsService.Login(credentials);
            } catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
           
            if (authToken != default)
            {
                AttackAuthTokenToResponse(HttpContext.Response, authToken);
                return Ok();
            }

            return BadRequest("Failed to login.");
        }

        public void AttackAuthTokenToResponse(HttpResponse httpResponse, string authToken)
        {
            httpResponse.Cookies.Append(Constants.AuthorizationTokenKey, authToken);
        }
    }
}