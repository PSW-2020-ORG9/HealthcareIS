using System;
using System.Collections.Generic;
using General.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        [Route("enc")]
        public IActionResult Encrypt(A obj)
        {
            return Ok(new JwtService().Encode(obj, DateTime.Now.AddSeconds(60).Ticks));
        }

        [HttpGet]
        [Route("dec/{obj}")]
        public IActionResult Decrypt(string obj)
        {
            return Ok(new JwtService().Decode<A>(obj));
        }
    }

    public class A : ICryptic
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public IDictionary<string, object> GetEncryptionAttributes()
        {
            return new Dictionary<string, object>(new []
            {
                new KeyValuePair<string, object>("Name", this.Name),
                new KeyValuePair<string, object>("Surname", this.Surname), 
            });
        }
    }
}