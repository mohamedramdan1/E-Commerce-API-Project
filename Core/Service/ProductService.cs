﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProducModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDTos;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTo>> GetAllBrandAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTo>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginationResult<ProductDTo>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(Specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(Products);
            var ProductCount = Products.Count();
            var CountSpecifications = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpecifications);
            return new PaginationResult<ProductDTo>(queryParams.PageIndex , ProductCount , TotalCount , Data);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypeAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);

        }

        public async Task<ProductDTo> GetProductByIdAsync(int Id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(Id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specifications);
            if (Product is null)
            {
                throw new ProductNotFoundException(Id);//404 Not found
            }
            return _mapper.Map<Product, ProductDTo>(Product);
        }
    }
}
