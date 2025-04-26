
using AutoMapper;
using DomainLayer.Contracts;
using E_Commer.Web.CustomExceptionHandlerMiddleware;
using E_Commer.Web.Extensions;
using E_Commer.Web.Fatories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using Presentation.Controllers;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;
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

            builder.Services.AddSwaggerServices(); // Swagger Registerations
            builder.Services.AddInfrastructureRegisterations(builder.Configuration); // Infrastructure Registerations
            builder.Services.AddApplicationServices(); // Services Registerations
            builder.Services.AddApiConfigurations(); // API Configurations
            #endregion

            var app = builder.Build();

            await app.SeedDataAsync(); // Seed Data

            // Configure the HTTP request pipeline.

            app.UseCustomExceptionHandler(); // Custom Exception Handler Middleware

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllers();

            app.UseAuthorization();

            app.Run();
        }
    }
}
