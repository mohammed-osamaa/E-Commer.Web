using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.ProductSpecifications
{
    internal class ProductWithBrandAndTypeSepcifications : BaseSpecifications<Product ,int>
    {
        public ProductWithBrandAndTypeSepcifications(ProductQueryParameters queryParameters) 
            : base(
                  P=>(!queryParameters.BrandId.HasValue || P.BrandId == queryParameters.BrandId)
                     && (!queryParameters.TypeId.HasValue || P.TypeId == queryParameters.TypeId)
                  && (string.IsNullOrEmpty(queryParameters.Search) || P.Name.ToLower().Contains(queryParameters.Search.ToLower()))
            )
        {
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);

            switch(queryParameters.ProductSorting)
            {
                case ProductSortings.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortings.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                case ProductSortings.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortings.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                default:
                    AddOrderBy(P => P.Id);
                    break;
            }
            ApplyPaging(queryParameters.PageSize , queryParameters.PageIndex);
        }
        public ProductWithBrandAndTypeSepcifications(int id) : base(P=>P.Id == id)
        {
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);
        }
    }
}
