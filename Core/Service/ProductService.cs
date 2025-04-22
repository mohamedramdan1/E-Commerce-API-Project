﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects;

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

        public async Task<IEnumerable<ProductDTo>> GetAllProductAsync(int? BrandId, int? TypeId)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(BrandId,  TypeId);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Specifications);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(Products);
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
            return _mapper.Map<Product, ProductDTo>(Product);
        }
    }
}
