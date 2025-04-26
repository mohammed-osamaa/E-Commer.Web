using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class AddInfrastructureRegisteration
    {
        public static IServiceCollection AddInfrastructureRegisterations(this IServiceCollection services , IConfiguration Configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
