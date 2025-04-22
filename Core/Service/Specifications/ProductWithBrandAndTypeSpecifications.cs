using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products With type And Brand
        public ProductWithBrandAndTypeSpecifications(int? BrandId, int? TypeId ,ProductSortingOptions sortingOptions) :
            base(P=>(!BrandId.HasValue || P.BrandId == BrandId) 
                    && (!TypeId.HasValue || P.TypeId ==TypeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P=>P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;
            }
        }

        public ProductWithBrandAndTypeSpecifications(int id):base(P=>P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
