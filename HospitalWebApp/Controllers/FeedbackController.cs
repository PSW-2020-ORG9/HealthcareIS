﻿using System;
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

        [HttpPut]
        public IActionResult Update(UserFeedback userFeedback)
        {
            return Ok(_userFeedbackService.Update(userFeedback));
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
                _userFeedbackService.Create(userFeedback);
            }
            catch (ReferenceConstraintException e)
            {
                return BadRequest("User with passed ID doesn't exist");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userFeedbackService.GetAll());

        }
    }
}
