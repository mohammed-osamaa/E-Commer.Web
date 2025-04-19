using Shared;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductServices
    {
        // Get all products
        Task<ProductPaginationData<ProductDto>> GetProductsAsync(ProductQueryParameters queryParameters);
        // Get product by id
        Task<ProductDto> GetProductByIdAsync(int id);
        // Get all Brands
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        // Get all Types
        Task<IEnumerable<TypeDto>> GetTypesAsync();
    }
}
