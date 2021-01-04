using Microsoft.AspNetCore.Mvc;
using User.API.Services.LocaleServices;

namespace User.API.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class CountryController:ControllerBase
    {
        private readonly CountryService countryService;

        public CountryController(CountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //TODO: Throw an exception when there are no countries.
            return Ok(countryService.GetAll());
        }
    }
}