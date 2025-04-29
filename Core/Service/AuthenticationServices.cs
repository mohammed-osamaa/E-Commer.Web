using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOS.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager
        , IMapper _mapper
        , IConfiguration _configuration) : IAuthenticationServices
    {
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDto>(user);
                //Generate Token (TODO)
                userDto.Token = await GenerateTokenAsync(user);
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
                userDto.Token = await GenerateTokenAsync(user);
                return userDto;
            }
            else
                throw new UnAuthorizedException();
        }
        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            // 1. Handle Payload (Claims)
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,user.UserName!),
                new Claim(ClaimTypes.Email ,user.Email!),
                new Claim(ClaimTypes.NameIdentifier ,user.Id),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            // 2. Handle Secret Key
            var secretKey = _configuration.GetSection("JWTOptions")["Key"];
            // Must Be represent Bytes
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            // 3. Signing Crediential (Algo + Key)
            var crediential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            // 4. Issuer , Audience
            var issuer = _configuration.GetSection("JWTOptions")["Issuer"];
            var audience = _configuration.GetSection("JWTOptions")["Audience"];
            // 5. Generate Token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: Claims,
                expires : DateTime.UtcNow.AddHours(1),
                signingCredentials: crediential
                );
            // Write Token 
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user is null)
                return false;
            return true;
        }

        public async Task<AddressDto> GetCurrentAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);
            if (user is null)
                throw new UserNOtFoundException(email);
            if (user.Address is null)
                throw new AddressNotFoundException(email);
            var address = _mapper.Map<AddressDto>(user.Address);
            return address;
        }

        public async Task<AddressDto> UpdateAddressAsync(string email, AddressDto updatedAddress)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);
            if (user is null)
                throw new UserNOtFoundException(email);
            if (user.Address is not null)
                _mapper.Map(updatedAddress, user.Address); // update same address
            else
                user.Address = _mapper.Map<Address>(updatedAddress); // Create new address and Assign it to user
            var result = await _userManager.UpdateAsync(user); // To Save New Address in Db (User table) 
            if (result.Succeeded)
                return _mapper.Map<AddressDto>(user.Address);
            else
                throw new Exception($"The user with email = {email} Is Not Update His Address.");
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNOtFoundException(email);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await GenerateTokenAsync(user);
            return userDto;
        }
    }
}
