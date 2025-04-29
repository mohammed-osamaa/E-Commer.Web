using AutoMapper;
using DomainLayer.Models.IdentityModels;
using Shared.DTOS.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<LoginDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName));
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
