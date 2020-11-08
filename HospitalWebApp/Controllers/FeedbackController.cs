using System;
using EntityFramework.Exceptions.Common;
using HealthcareBase.Service.ValidationService;
using Microsoft.AspNetCore.Mvc;
using Model.CustomExceptions;
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
            try
            {
                UserFeedbackValidator.validate(userFeedback);
                return Ok(_userFeedbackService.Update(userFeedback));
            }
            catch(ReferenceConstraintException e)
            {
                return BadRequest(e.Message);
            }
            catch(ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        ///  Creates new user feedback.
        /// </summary>
        /// <param name="userFeedback"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(UserFeedback userFeedback)
        {
            
            try
            {
                UserFeedbackValidator.validate(userFeedback);
                UpdateUserFeedbackDate(userFeedback);
                _userFeedbackService.Create(userFeedback);
            }
            catch (ReferenceConstraintException)
            {
                return BadRequest("User with passed ID doesn't exist");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
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

        private static void UpdateUserFeedbackDate(UserFeedback userFeedback)
        {
            userFeedback.Date=DateTime.Now;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userFeedbackService.GetAll());
        }
    }
}
