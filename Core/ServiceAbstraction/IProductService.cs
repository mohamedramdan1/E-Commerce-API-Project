using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects.ProductModuleDTos;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //Get All Product
        Task<PaginationResult<ProductDTo>> GetAllProductAsync(ProductQueryParams queryParams);

        //Get Product By Id
        Task<ProductDTo> GetProductByIdAsync(int Id);

        //Get All Types
        Task<IEnumerable<TypeDTO>> GetAllTypeAsync();

        //Get All Brands
        Task<IEnumerable<BrandDTo>> GetAllBrandAsync();
    }
}
