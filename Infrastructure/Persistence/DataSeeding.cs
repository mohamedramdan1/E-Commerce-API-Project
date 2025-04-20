using System;
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
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigartions = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigartions.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // For ProductBrand
                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // Convert From string to C# Object [list<ProductBrand>] using Deserlized
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    //Save in Db
                    if (ProductBrands is not null && ProductBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }

                // For ProductType
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert From string to C# Object [list<ProductType>] using Deserlized
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }

                // For Product
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert From string to C# Object [list<Product>] using Deserlized
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
