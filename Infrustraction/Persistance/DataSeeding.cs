using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DataSeeding(StoreDbContext _dbContext 
        , UserManager<ApplicationUser> _userManager
        , RoleManager<IdentityRole> _roleManager
        , StoreIdentityDbContext _storeIdentityDbContext) : IDataSeeding
    {
        public async Task IdentitySeedDateAsync()
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
                    var User01 = new ApplicationUser
                    {
                        Email = "mohamed@gmail.com",
                        DisplayName = "Mohamed Osamaa",
                        UserName = "mohamedOsamaa",
                        PhoneNumber = "01012345678",
                    };
                    var User02 = new ApplicationUser
                    {
                        Email = "Isla@gmail.com",
                        DisplayName = "Islam Osamaa",
                        UserName = "IslamOsamaa",
                        PhoneNumber = "01012345678",
                    };

                    await _userManager.CreateAsync(User01, "Admin01@");
                    await _userManager.CreateAsync(User02, "Admin02@");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
                await _storeIdentityDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                // Handle exceptions (e.g., log the error)
            }
        }

        public async Task SeedDataAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!await _dbContext.ProductBrands.AnyAsync())
                {
                    //string jsonData = File.ReadAllText("..\\Infrustraction\\Persistance\\Data\\DataSeed\\brands.json");
                    //var brands = JsonSerializer.Deserialize<List<ProductBrand>>(jsonData);
                    //if(brands is not null && brands.Any())
                    //    _dbContext.ProductBrands.AddRange(brands);
                    var jsonData = File.OpenRead("..\\Infrustraction\\Persistance\\Data\\DataSeed\\brands.json");
                    var brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(jsonData);
                    if (brands is not null && brands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(brands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var jsonData = File.OpenRead("..\\Infrustraction\\Persistance\\Data\\DataSeed\\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(jsonData);
                    if (types is not null && types.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(types);
                }
                if (!_dbContext.Products.Any())
                {
                    var jsonData = File.OpenRead("..\\Infrustraction\\Persistance\\Data\\DataSeed\\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(jsonData);
                    if (products is not null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
            }

        }
    }
}
