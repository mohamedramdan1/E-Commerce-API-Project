using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProducModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
       StoreIdentityDbContext _IdentityDbContext) : IDataSeeding
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

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Ramadan",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedRamadan"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ramadan",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaRamadan"
                    };


                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                    await _IdentityDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
