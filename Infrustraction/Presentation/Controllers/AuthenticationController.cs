using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        // Check Email is Existed or Not
        [HttpGet("CheckEmailExists/{email}")] // Get BaseUrl/api/authentication/CheckEmailExists/{email}
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var result = await _serviceManager.AuthenticationServices.CheckEmailExistsAsync(email);
            return Ok(result);
        }
        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); // User that is logged Now 
            var user = await _serviceManager.AuthenticationServices.GetUserAsync(email!);
            return Ok(user);
        }
        [HttpGet("GetCurrentAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationServices.GetCurrentAddressAsync(email!);
            return Ok(address);
        }
        [HttpPut("UpdateAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto updatedAddress)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationServices.UpdateAddressAsync(email!, updatedAddress);
            return Ok(address);
        }
    }
}
