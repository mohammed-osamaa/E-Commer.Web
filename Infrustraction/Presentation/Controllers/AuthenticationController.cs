using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        // Login take LoginDto and return a UserDto
        [HttpPost("login")] // Post BaseUrl/api/authentication/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var UserDto = await _serviceManager.AuthenticationServices.Login(loginDto);
            return Ok(UserDto);
        }
        [HttpPost("Register")] // Post BaseUrl/api/authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
         {
            var UserDto = await _serviceManager.AuthenticationServices.Register(registerDto);
            return Ok(UserDto);
        }
    }
}
