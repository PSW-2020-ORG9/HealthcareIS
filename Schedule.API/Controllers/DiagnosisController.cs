using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("schedule/[controller]")]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisService _diagnosisService;

        public DiagnosisController(IDiagnosisService diagnosisService)
        {
            this._diagnosisService = diagnosisService;
        }
        
        [HttpGet]
        [Route("{diagnosisId}")]
        public IActionResult Find([FromRoute] int diagnosisId)
        {
            return Ok(_diagnosisService.Find(diagnosisId));
        }

        [HttpPost]
        public IActionResult Find(IEnumerable<int> diagnosisIds)
        {
            return Ok(_diagnosisService.Find(diagnosisIds));
        }
    }
}