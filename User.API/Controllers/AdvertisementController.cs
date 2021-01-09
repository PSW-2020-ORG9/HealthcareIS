using Microsoft.AspNetCore.Mvc;
using User.API.Services.PromotionsService;

namespace User.API.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly AdvertisementService _advertisementService;
        
        public AdvertisementController(AdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        [Route("active")]
        public IActionResult GetAllActive() 
            => Ok(_advertisementService.GetAllActive());
    }
}