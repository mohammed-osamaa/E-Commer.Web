using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DTOS.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager
        , IMapper _mapper) : IAuthenticationServices
    {
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDto>(user);
                //Generate Token (TODO)
                userDto.Token = GenerateTokenAsync(user);
                return userDto;
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }


        public async Task<UserDto> Login(LoginDto loginDto)
        {
            //if Email is Existed Or Not 
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user is null)
                throw new UserNOtFoundException(loginDto.Email);
            //if Password is Correct Or Not
            var CheckPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(CheckPassword)
            { 
                var userDto = _mapper.Map<UserDto>(user);
                //Generate Token (TODO)
                userDto.Token = GenerateTokenAsync(user);
                return userDto;
            }
            else
                throw new UnAuthorizedException();
        }
        private static string GenerateTokenAsync(ApplicationUser user)
        {
            return "Token TODO";
        }
    }
}
