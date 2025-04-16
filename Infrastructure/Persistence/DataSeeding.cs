﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                // For ProductBrand
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // Convert From string to C# Object [list<ProductBrand>] using Deserlized
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                }

                // For ProductType
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert From string to C# Object [list<ProductType>] using Deserlized
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                }

                // For Product
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert From string to C# Object [list<Product>] using Deserlized
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                        _dbContext.Products.AddRange(Products);
                }

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
