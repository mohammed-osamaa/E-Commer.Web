
using AutoMapper;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using Presentation.Controllers;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using System.Threading.Tasks;

namespace E_Commer.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(ProductsController).Assembly);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
             
            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManger>();
            #endregion

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IDataSeeding>().SeedDataAsync();    

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseAuthorization();

            app.Run();
        }
    }
}
