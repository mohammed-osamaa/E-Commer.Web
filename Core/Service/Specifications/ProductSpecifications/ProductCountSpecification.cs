using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.ProductSpecifications
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParameters queryParameters)
            : base(
                  P => (!queryParameters.BrandId.HasValue || P.BrandId == queryParameters.BrandId)
                     && (!queryParameters.TypeId.HasValue || P.TypeId == queryParameters.TypeId)
                  && (string.IsNullOrEmpty(queryParameters.Search) || P.Name.ToLower().Contains(queryParameters.Search.ToLower()))
            )
        {
        }
    }
}
