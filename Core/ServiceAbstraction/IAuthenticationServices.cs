using Shared.DTOS.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationServices
    {
        // Login method Take Email and Password as parameters Returns (Email ,DisplayName and Token)
        Task<UserDto> Login(LoginDto loginDto);
        // Register method Take Email, Username,PhoneNumber , DisplayName , Password as parameters Returns (Email ,DisplayName and Token)
        Task<UserDto> Register(RegisterDto registerDto);
        // First, I Need to create a DTO for the Login and Register methods
        // IdentityDto => (Email , DisplayName And Token)
        // LoginDto => (Email and Password)
        // RegisterDto => (Email, Username, PhoneNumber, DisplayName And Password) 

        // I Check If Email is existed or not 
        Task<bool> CheckEmailExistsAsync(string email);

        // I Get Current User Address => By User 
        Task<AddressDto> GetCurrentAddressAsync(string email); // get Email by User No Take Email Parameter

        // I Update Address 
        Task<AddressDto> UpdateAddressAsync(string email , AddressDto updatedAddress);
        // I Get User
        Task<UserDto> GetUserAsync(string email);

    }
}
