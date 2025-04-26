using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Service.Specifications.ProductSpecifications;
using ServiceAbstraction;
using Shared;
using Shared.DTOS.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper ) : IProductServices
    {
        public async Task<ProductPaginationData<ProductDto>> GetProductsAsync(ProductQueryParameters queryParameters)
        {
            // Get all products With Brand and Type
            var specification = new ProductWithBrandAndTypeSepcifications(queryParameters);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(specification);
            var Data = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products);
            // Get total count of products
            var totalCount = await Repo.CountAsync(new ProductCountSpecification(queryParameters));
            return new ProductPaginationData<ProductDto>(queryParameters.PageIndex , Data.Count(),totalCount, Data);
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            // Get product by id With Brand and Type
            var specification = new ProductWithBrandAndTypeSepcifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            if (Product is not null)
                return _mapper.Map<Product, ProductDto>(Product);
            else
                throw new NotFoundProductException(id);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }
    }
}
