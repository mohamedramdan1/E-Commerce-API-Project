using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProducModule;
using Shared;

namespace Service.Specifications
{
    internal class ProductCountSpecification : BaseSpecifications<Product , int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            
        }
    }
}
