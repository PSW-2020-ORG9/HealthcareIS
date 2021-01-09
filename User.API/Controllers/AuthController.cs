using General.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.API.DTOs;
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
        [Route("login/patient")]
        public IActionResult LoginPatient(LoginCredentials credentials)
        {
            string authToken = _credentialsService.LoginPatient(credentials);
            if (authToken != default)
            {
                AttackAuthTokenToResponse(HttpContext.Response, authToken);
                return Ok();
            }

            return BadRequest("Failed to login.");
        }

        [HttpPost]
        [Route("login/doctor")]
        public IActionResult LoginDoctor(LoginCredentials credentials)
        {
            string authToken = _credentialsService.LoginDoctor(credentials);
            if (authToken != default)
            {
                AttackAuthTokenToResponse(HttpContext.Response, authToken);
                return Ok();
            }

            return BadRequest("Failed to login.");
        }

        public void AttackAuthTokenToResponse(HttpResponse httpResponse, string authToken)
        {
            httpResponse.Cookies.Append(JwtManager.AuthorizationTokenKey, authToken);
        }
    }
}