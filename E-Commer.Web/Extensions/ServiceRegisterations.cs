using E_Commer.Web.Fatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commer.Web.Extensions
{
    public static class ServiceRegisterations
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
           Services.AddEndpointsApiExplorer();
           Services.AddSwaggerGen();
           return Services;
        }

        public static IServiceCollection AddApiConfigurations(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateValidationErrorsResponse;

            });
            return Services;
        }
    }
}
