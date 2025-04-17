using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper ) : IProductServices
    {
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products);
            return productDtos;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            if (Product is not null)
                return _mapper.Map<Product, ProductDto>(Product);
            return null!;
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }
    }
}
