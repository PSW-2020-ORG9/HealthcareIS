using HealthcareBase.Service.MiscellaneousService;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController:ControllerBase
    {
        private readonly CityService cityService;
        
        public CityController(CityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        [Route("by-country/{id}")]
        public IActionResult GetByCountry(int id)
        {
            // TODO: Throw an exception when there's no cities in a country with a given id.
            return Ok(cityService.GetByCountry(id));
        }
    }
}