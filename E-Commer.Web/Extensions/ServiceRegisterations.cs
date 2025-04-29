using E_Commer.Web.Fatories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
        public static IServiceCollection AddJWTConfigs(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // How to Validate Token
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true ,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],
                    ValidAudience = configuration["JWTOptions:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:Key"]!))
                };
            });
            return Services;
        }


    }
}
