using DomainLayer.Contracts;
using DomainLayer.Models;
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
        public void SeedData()
        {
            try
            {
                if(_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    string jsonData = File.ReadAllText("Data/SeedData.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(jsonData);
                    if(brands is not null && brands.Any())
                        _dbContext.ProductBrands.AddRange(brands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    string jsonData = File.ReadAllText("Data/SeedData.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(jsonData);
                    if (types is not null && types.Any())
                        _dbContext.ProductTypes.AddRange(types);
                }
                if (!_dbContext.Products.Any())
                {
                    string jsonData = File.ReadAllText("Data/SeedData.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(jsonData);
                    if (products is not null && products.Any())
                        _dbContext.Products.AddRange(products);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
            }

        }
    }
}
