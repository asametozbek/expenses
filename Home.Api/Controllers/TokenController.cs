using Entity;
using Home.Api.Extensions;
using Home.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IJwtAuth jwtAuth;

    
        public TokenController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }
        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
