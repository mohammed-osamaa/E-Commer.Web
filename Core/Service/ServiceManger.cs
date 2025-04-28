using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManger(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository , UserManager<ApplicationUser> _userManager , IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductServices> _productServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
        private readonly Lazy<IBasketServices> _basketServices = new Lazy<IBasketServices>(() => new BasketServices( basketRepository , _mapper));
        private readonly Lazy<IAuthenticationServices> authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(_userManager,_mapper, _configuration));
        public IProductServices ProductServices => _productServices.Value;
        public IBasketServices BasketServices => _basketServices.Value;
        public IAuthenticationServices AuthenticationServices => authenticationServices.Value;
    }
}
