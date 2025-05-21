using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDTos;

namespace Presentation.Controllers
{

    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Get All products
        //GET baseUrl/api/Products
        //ProductQueryParams that class have ll paermter we send to End point GetAllProducts here it is complex object so we must use [fromQuery] becouse [HTTPGET] desnot have body
        [HttpGet]  
        public async Task<ActionResult<PaginationResult<ProductDTo>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductAsync(queryParams);
            return Ok(Products);
        }


        //Get product By Id
        //GET baseUrl/api/Products/10
        [HttpGet("{id:int}")]   
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }


        //Get All Types
        //GET baseUrl/api/Products/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            var Types =await _serviceManager.ProductService.GetAllTypeAsync();
            return Ok(Types);
        }


        //Get All Brands
        //GET baseUrl/api/Products/brand
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTo>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandAsync();
            return Ok(Brands);
        }

    }
}
