
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using System.Threading.Tasks;

namespace E_Commer.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
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
