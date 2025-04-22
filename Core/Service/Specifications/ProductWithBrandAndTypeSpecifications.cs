using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products With type And Brand
        public ProductWithBrandAndTypeSpecifications(int? BrandId, int? TypeId) :
            base(P=>(!BrandId.HasValue || P.BrandId == BrandId) 
                    && (!TypeId.HasValue || P.TypeId ==TypeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
