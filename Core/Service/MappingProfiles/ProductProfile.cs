using AutoMapper;
using DomainLayer.Models;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.BrandName, opt => opt.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.TypeName, opt => opt.MapFrom(S => S.ProductType.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
