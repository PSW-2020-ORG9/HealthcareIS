using HealthcareBase.Service.MiscellaneousService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
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