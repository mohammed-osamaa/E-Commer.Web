using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManger(IUnitOfWork _unitOfWork,IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductServices> _productServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
        public IProductServices ProductServices => _productServices.Value;
    }
}
