using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.ProductSpecifications
{
    internal class ProductWithBrandAndTypeSepcifications : BaseSpecifications<Product ,int>
    {
        public ProductWithBrandAndTypeSepcifications(int? BrandId , int? TypeId) 
            : base(
                  P=>(!BrandId.HasValue || P.BrandId == BrandId)
                     && (!TypeId.HasValue || P.TypeId == TypeId)
            )
        {
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);
        }
        public ProductWithBrandAndTypeSepcifications(int id) : base(P=>P.Id == id)
        {
            AddIncludes(P => P.ProductBrand);
            AddIncludes(P => P.ProductType);
        }
    }
}
