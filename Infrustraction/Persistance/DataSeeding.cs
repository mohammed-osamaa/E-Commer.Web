using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
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
