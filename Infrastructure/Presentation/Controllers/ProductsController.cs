using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //baseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get All products
        //GET baseUrl/api/Products
        [HttpGet]  
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetAllProducts()
        {
            var Products = await _serviceManager.ProductService.GetAllProductAsync();
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
