using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Updates the given <see cref="UserFeedback"/>
        /// </summary>
        /// <param name="userFeedback"></param>
        /// <returns>An <see cref="IActionResult"/> representing a result of the operation.</returns>
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
            catch (ReferenceConstraintException e)
            {
                return BadRequest("ReferenceConstraintException");
            }

            return Ok();
        }

        /// <summary>
        /// Publishes a UserFeedback with a given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="IActionResult"/> representing a result of the operation.</returns>
        [Route("publish/{id}")]
        [HttpGet]
        public IActionResult Publish(int id)
        {
            //TODO: Check if current user is admin
            if(_userFeedbackService.Publish(id))
            {
                return Ok("Feedback successfully published.");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userFeedbackService.GetAll());
        }
    }
}
