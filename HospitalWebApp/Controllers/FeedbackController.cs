using System;
using EntityFramework.Exceptions.Common;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.UserFeedback;
using HealthcareBase.Service.UsersService.UserFeedbackService;
using HospitalWebApp.Adapters;
using HospitalWebApp.Dtos;
using HospitalWebApp.Validators;
using Microsoft.AspNetCore.Mvc;

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
        ///     Creates a new <see cref="UserFeedback"/> from the given <see cref="UserFeedbackDto"/> object. 
        /// </summary>
        /// <param name="userFeedbackDto"></param>
        /// <returns>
        ///     <see cref="OkObjectResult"/> with the newly added UserFeedback, if the DTO is valid and object successfully
        ///     added to the database.
        ///     <see cref="BadRequestResult"/> with the error message, if the DTO argument is not valid.
        /// </returns>
        [HttpPost]
        public IActionResult Create(UserFeedbackDto userFeedbackDto)
        {
            try
            {
                // TODO Anonymous user logic
                UserFeedbackValidator.Validate(userFeedbackDto);
                var userFeedback = UserFeedbackAdapter.DtoToObject(userFeedbackDto);
                return Ok(_userFeedbackService.Create(userFeedback));
            }
            catch (ReferenceConstraintException)
            {
                return BadRequest("User with the given ID doesn't exist.");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Publishes a UserFeedback with a given id.
        /// </summary>
        /// <param name="id"> ID of the <see cref="UserFeedback"/> to be published.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> with the success message if successful.
        /// <see cref="BadRequestResult"/> if no such UserFeedback is found.
        /// </returns>
        [Route("publish/{id}")]
        [HttpGet]
        public IActionResult Publish(int id)
        {
            //TODO: Check if current user is admin
            if(_userFeedbackService.Publish(id)) 
                return Ok("Feedback successfully published.");
            return BadRequest();
        }
        
        /// <summary>
        /// Returns a list of all <see cref="UserFeedback"/> objects from the database
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/> with the list of Feedbacks inside.
        /// </returns>
        [HttpGet]
        public IActionResult GetAll()
            => Ok(_userFeedbackService.GetAll());

        /// <summary>
        /// Returns a list of all <see cref="UserFeedback"/> objects from the database, where
        /// <see cref="UserFeedback.isPublished"/> is True
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/> with the given feedback list inside of it.
        /// </returns>
        [HttpGet]
        [Route("published")]
        public IActionResult GetAllPublished()
            => Ok(_userFeedbackService.GetAllPublished());
    }
}
