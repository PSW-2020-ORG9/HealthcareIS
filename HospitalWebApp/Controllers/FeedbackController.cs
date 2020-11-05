using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model.Users.Generalities;
using Model.Users.UserAccounts;
using Model.Users.UserFeedback;
using Service.UsersService.UserFeedbackService;

namespace HospitalWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly UserFeedbackService _userFeedbackService;

        public FeedbackController(UserFeedbackService userFeedbackService)
        {
            _userFeedbackService = userFeedbackService;
        }
        
        [HttpPut]
        public IActionResult Update(UserFeedback userFeedback)
        {
            return Ok(_userFeedbackService.Update(userFeedback));
        }

        [HttpPost]
        public IActionResult Post(UserFeedback userFeedback)
        {
            try
            {
                _userFeedbackService.Create(userFeedback);
            }
            catch(ReferenceConstraintException e)
            {
                return BadRequest("ReferenceConstraintException");
            }
            return Ok();
        }
    }
}
