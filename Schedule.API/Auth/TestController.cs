﻿using Microsoft.AspNetCore.Mvc;

namespace Schedule.API.Auth
{
    [ApiController]
    [Route("schedule/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        [Route("enc")]
        public IActionResult Encrypt(UserToken obj)
        {
            return Ok(new JwtManager().Encode(obj));
        }

        [HttpGet]
        [Route("dec/{obj}")]
        public IActionResult Decrypt(string obj)
        {
            return Ok(new JwtManager().Decode<UserToken>(obj));
        }
    }
}