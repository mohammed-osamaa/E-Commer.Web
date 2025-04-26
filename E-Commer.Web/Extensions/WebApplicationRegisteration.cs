using DomainLayer.Contracts;
using E_Commer.Web.CustomExceptionHandlerMiddleware;
using System.Threading.Tasks;

namespace E_Commer.Web.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IDataSeeding>().SeedDataAsync();
        }
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptiomHandlerMiddleware>();
        }
    }
}
