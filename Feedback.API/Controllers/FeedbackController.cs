using Feedback.API.DTOs;
using Feedback.API.Mappers;
using Feedback.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Feedback.API.Controllers
{
    [ApiController]
    [Route("feedbacks/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IUserFeedbackService _userFeedbackService;

        public FeedbackController(IUserFeedbackService userFeedbackService)
        {
            _userFeedbackService = userFeedbackService;
        }

        [HttpPost]
        public IActionResult Create(UserFeedbackDTO userFeedbackDto)
        {
            try
            {
                var userFeedback = UserFeedbackMapper.DtoToObject(userFeedbackDto);
                return Ok(_userFeedbackService.Create(userFeedback));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("publish/{id}")]
        [HttpGet]
        public IActionResult Publish(int id)
        {
            //TODO: Check if current user is admin
            try
            {
                _userFeedbackService.Publish(id);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Feedback successfully published.");
            
        }
        
        [HttpGet]
        public IActionResult GetAll()
            => Ok(_userFeedbackService.GetAll());

        [HttpGet]
        [Route("published")]
        public IActionResult GetAllPublished()
            => Ok(_userFeedbackService.GetAllPublished());
    }
}
