using Crims.Core.Helpers;
using Crims.Core.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Crims.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(Assembly.Load("Crims.Core"));
            services.AddSingleton<IPasswordHelper, PasswordHelper>();
            return services;
        }
    }
}
