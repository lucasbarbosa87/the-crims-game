using Crims.Data.Persistence;
using Crims.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
    configuration.GetConnectionString("database")
    );

                //options.UseNpgsql(
                //    configuration.GetConnectionString("database")
                //    );
            });
            services.AddScoped<ApplicationDbContext>();


            services.AddScoped<UserRepository>();
            services.AddScoped<TokenRepository>();
            services.AddScoped<UserRoleRepository>();
            services.AddScoped<EstablishmentRepository>();

            return services;
        }
    }
}
