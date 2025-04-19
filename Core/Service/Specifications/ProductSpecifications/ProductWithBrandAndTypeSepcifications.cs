using DomainLayer.Models;
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
        public ProductWithBrandAndTypeSepcifications(int? BrandId , int? TypeId,ProductSortings productSorting) 
            : base(
                  P=>(!BrandId.HasValue || P.BrandId == BrandId)
                     && (!TypeId.HasValue || P.TypeId == TypeId)
            )
        {
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);

            switch(productSorting)
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

        }
        public ProductWithBrandAndTypeSepcifications(int id) : base(P=>P.Id == id)
        {
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);
        }
    }
}
